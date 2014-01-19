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
using GuiLib;

using CommsLib.Util;
using Tulip.Lib;
using System.Data.Entity;
namespace Tulip
{
    public partial class Main : Form
    {
        IMaster master;
        IMasterScan classScan;

        public static Lib.Manager Manager;
        //Tulip.TulipEntities _context;

        public Main()
        {
            InitializeComponent();

            //TextBoxTextWriter tx = new TextBoxTextWriter(txtLog, System.Threading.Thread.CurrentThread);
            //Console.SetOut(tx);
            
            
             //_context = new TulipEntities();

            
            IDNP3Manager mgr = DNP3ManagerFactory.CreateManager();
            Manager = new Lib.Manager(mgr);
            
            StringBuilderLogHandler SBLH = (StringBuilderLogHandler) StringBuilderLogHandler.Instance;
            SBLH.SB = Manager.Log;

            mgr.AddLogHandler(SBLH); //this is optional

            Manager.TulipContext.Channels.Load();
            Manager.TulipContext.Outstations.Load();
            Manager.TulipContext.Points.Load();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            master.GetIntegrityScan().Demand();
            //future.Listen((result) => Console.WriteLine("Result: " + result));
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
            List<ChannelWrapper> cws = Manager.Channels;

            tblChannelStatus_Model.Rows.Clear();
            foreach (ChannelWrapper C in cws)
            {

                tblChannelStatus_Model.Rows.Add(new XPTable.Models.Row(new[] { "", C.Model.Id.ToString(), C.Model.Name, C.state.ToString() }));
            }
        }

        private void refresh_outstations(OutstationWrapper updated)
        {
            this.BeginInvoke(new Action(refresh_outstations2));
        }

        private void refresh_outstations2()//OutstationWrapper updated)
        {
            List<OutstationWrapper> cws = Manager.Outstations;

            tblOutstationStatus_Model.Rows.Clear();
            foreach (OutstationWrapper C in cws)
            {

                tblOutstationStatus_Model.Rows.Add(new XPTable.Models.Row(new[] { "", C.Model.Id.ToString(), C.Model.Name, C.state.ToString() }));
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmLog log = new frmLog(Manager.Log.ToString());
            log.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // TODO: dodge
            /* 1 - create the channels collection */

            var Channels = from os in Manager.TulipContext.Channels
                           select os;

            var Outstations = from os in Manager.TulipContext.Outstations
                              select os;

            List<Channel> CC = Channels.ToList();

            // TODO: DONT START STUFF UNTIL WINDOW HANDLE HAS BEEN CREATED OR WE WILL LOCK SHIT UP


            Manager.AddChannels(Channels.ToList());
            Manager.OnChannelStateChange += this.refresh_channels;
            refresh_channels2();

            Manager.AddOutstations(Outstations.ToList());
            Manager.OnOutstationStateChange += this.refresh_outstations;
            refresh_outstations2();

            // TODO: re-instate this to allow refreshing of datatables?
            //Manager.OnOutstationMeasurementReceived += this.new_measurement_handler;

            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmPointSummary frmPS = new frmPointSummary(Manager);
            frmPS.Show();
        }



        private void channelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmChannelSummary().Show();
        }

        private void outstationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmOutstationSummary().Show();
        }

        private void pointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmPointConfiguration(Manager.TulipContext).Show();
        }
    }
}
