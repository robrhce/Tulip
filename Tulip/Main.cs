using DNP3.Interface;
using DNP3.Adapter;



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUtil;

namespace Tulip
{
    public partial class Main : Form
    {
        IMaster master;
        IMasterScan classScan;


        public Main()
        {
            InitializeComponent();



            Tulip.TulipEntities TDS = new TulipEntities();

            var Outstations = from os in TDS.Outstations
                              select os;

            var Channels = from os in TDS.Channels
                           select os;

            TextBoxTextWriter tx = new TextBoxTextWriter(txtLog, System.Threading.Thread.CurrentThread);
            Console.SetOut(tx);

            foreach (var OS1 in Outstations)
            {
                //txtLog.AppendText(OS1.Id + " - " + OS1.Name + " - " + OS1.Description + Environment.NewLine);

                var FirstChannel = from fc in TDS.OutstationChannelMappings
                                   from fd in TDS.Channels
                                   where fc.OutstationID == fd.Id
                                   select fc;// new {OCM = fc, cc = fd };

                var fc1 = FirstChannel.First().Channel;


                Console.WriteLine(OS1.Id + " - " + OS1.Name + " - " + OS1.Description + Environment.NewLine);
                Console.WriteLine("Channel: " + fc1.Name + " - " + fc1.ConnectionString);



            }





            IDNP3Manager mgr = DNP3ManagerFactory.CreateManager();
            mgr.AddLogHandler(PrintingLogAdapter.Instance); //this is optional
            var channel = mgr.AddTCPClient("client", LogLevel.Info, TimeSpan.FromSeconds(5), "127.0.0.1", 8888);
            SerialSettings ss = new SerialSettings("com8", 9600, 8, 1, Parity.NONE, FlowControl.NONE);

            //var channel = mgr.AddSerial("SER0", LogLevel.Info, TimeSpan.FromSeconds(5), ss);

            //optionally, add a listener for the channel state
            channel.AddStateListener(state => Console.WriteLine("Client state: " + state));

            var config = new MasterStackConfig();
            config.master.integrityPeriod = TimeSpan.FromSeconds(-1);
            config.master.taskRetryPeriod = TimeSpan.FromSeconds(60);
            config.link.localAddr = 30001;
            config.link.remoteAddr = 17;
            config.link.timeout = TimeSpan.FromSeconds(10);
            config.link.useConfirms = true; //setup your stack configuration here.
            config.app.rspTimeout = TimeSpan.FromSeconds(50);

            master = channel.AddMaster("master", LogLevel.Interpret, PrintingMeasurementHandler.Instance, config);

            /*
            var config2 = new MasterStackConfig();

            config2.master.integrityPeriod = TimeSpan.FromSeconds(60);
            config2.link.localAddr = 3;
            config2.link.remoteAddr = 191;
            config2.link.useConfirms = true; //setup your stack configuration here.
            var master2 = channel.AddMaster("master", LogLevel.Interpret, PrintingMeasurementHandler.Instance, config2);

            */

            //optionally, add a listener for the stack state
            master.AddStateListener(state => Console.WriteLine("Master state: " + state));

            var classMask = PointClassHelpers.GetMask(PointClass.PC_CLASS_1, PointClass.PC_CLASS_2, PointClass.PC_CLASS_3);
            classScan = master.AddClassScan(classMask, TimeSpan.FromSeconds(-1), TimeSpan.FromSeconds(-1));
            //var integrityScan = master.GetIntegrityScan();

            master.Enable(); // enable communications
            //master2.Enable();

            Console.WriteLine("Enter an index to send a command");

            // Test sequence




            /*
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "c":
                        var crob = new ControlRelayOutputBlock(ControlCode.PULSE, 1, 100, 100);
                        var future = master.GetCommandProcessor().SelectAndOperate(crob, 0);
                        future.Listen((result) => Console.WriteLine("Result: " + result));
                        break;
                    case "i":
                        integrityScan.Demand();
                        break;
                    case "e":
                        classScan.Demand();
                        break;
                    case "x":
                        return 0;
                    default:
                        break;
                }
            } 
             * */


        }

        private void button1_Click(object sender, EventArgs e)
        {
            master.GetIntegrityScan().Demand();
            //future.Listen((result) => Console.WriteLine("Result: " + result));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool State = radioButton1.Checked;
            UInt16 point = Convert.ToUInt16(textBox1.Text);
            ControlRelayOutputBlock crob;

            if (State)
                crob = new ControlRelayOutputBlock(ControlCode.LATCH_ON, 1, 0, 0);
            else
                crob = new ControlRelayOutputBlock(ControlCode.LATCH_OFF, 1, 0, 0);

            var future = master.GetCommandProcessor().DirectOperate(crob, point);
            future.Listen((result) => Console.WriteLine("Result: " + result));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var aob = new AnalogOutputInt32(8008);
            var future = master.GetCommandProcessor().DirectOperate(aob, 0);
            future.Listen((result) => Console.WriteLine("Result: " + result));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            classScan.Demand();
        }
    }
}
