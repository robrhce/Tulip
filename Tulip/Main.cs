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

using CommsLib.Util;
using Tulip.Lib;

namespace Tulip
{
    public partial class Main : Form
    {
        IMaster master;
        IMasterScan classScan;

        Lib.Manager _manager;
        Tulip.TulipEntities TDS;

        public Main()
        {
            InitializeComponent();

            //TextBoxTextWriter tx = new TextBoxTextWriter(txtLog, System.Threading.Thread.CurrentThread);
            //Console.SetOut(tx);
            
            
             TDS = new TulipEntities();

            
            IDNP3Manager mgr = DNP3ManagerFactory.CreateManager();
            _manager = new Lib.Manager(mgr);
            
            StringBuilderLogHandler SBLH = (StringBuilderLogHandler) StringBuilderLogHandler.Instance;
            SBLH.SB = _manager.Log;

            mgr.AddLogHandler(SBLH); //this is optional
            

            






            //refresh_channels2();

            // TODO: check if unsolicited messages are read if the master is disabled
            //optionally, add a listener for the channel state
            //TODO 

            
            /*
            var config2 = new MasterStackConfig();

            config2.master.integrityPeriod = TimeSpan.FromSeconds(60);
            config2.link.localAddr = 3;
            config2.link.remoteAddr = 191;
            config2.link.useConfirms = true; //setup your stack configuration here.
            var master2 = channel.AddMaster("master", LogLevel.Interpret, PrintingMeasurementHandler.Instance, config2);

            */

            //optionally, add a listener for the stack state
            
            //var integrityScan = master.GetIntegrityScan();

            
            //master2.Enable();

            //Console.WriteLine("Enter an index to send a command");

            // Test sequence


            //frmChannelSummary fps = new frmChannelSummary();
            //fps.Show();

            frmOutstationSummary fos = new frmOutstationSummary();
            fos.Show();

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


        private void refresh_channels(ChannelWrapper updated)
        {
            this.BeginInvoke(new Action(refresh_channels2));
        }

        private void refresh_channels2()
        {
            List<ChannelWrapper> cws = _manager.Channels;

            tblChannelStatus_Model.Rows.Clear();
            foreach (ChannelWrapper C in cws)
            {

                tblChannelStatus_Model.Rows.Add(new XPTable.Models.Row(new[] { "", C.Model.Id.ToString(), C.Model.Name, C.state.ToString() }));
            }
        }

        private void refresh_outstations(OutstationWrapper updated)
        {
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmLog log = new frmLog(_manager.Log.ToString());
            log.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // TODO: dodge
            /* 1 - create the channels collection */

            var Channels = from os in TDS.Channels
                           select os;

            var Outstations = from os in TDS.Outstations
                              select os;

            List<Channel> CC = Channels.ToList();

            // TODO: DONT START STUFF UNTIL WINDOW HANDLE HAS BEEN CREATED OR WE WILL LOCK SHIT UP


            _manager.AddChannels(Channels.ToList());
            _manager.OnChannelStateChange += this.refresh_channels;

            _manager.AddOutstations(Outstations.ToList());
            _manager.OnOutstationStateChange += this.refresh_outstations;
        }
    }
}
