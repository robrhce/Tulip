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

            Manager.OnOutstationMeasurementReceived += this.new_measurement_handler;

            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmPointSummary frmPS = new frmPointSummary(Manager);
            frmPS.Show();
        }

        private void new_measurement_handler()
        {
            DateTime update_time = DateTime.UtcNow;
            foreach (OutstationWrapper ow in Manager.Outstations)
            {
                if (ow.NewAnalogs.Count > 0)
                {
                    foreach (IndexedValue<Analog> ana in ow.NewAnalogs)
                    {
                        update_analog(ow, ana, update_time);
                    }
                    ow.NewAnalogs.Clear();
                }
                if (ow.NewBinaries.Count > 0)
                {
                    foreach (IndexedValue<Binary> bin in ow.NewBinaries)
                    {
                        update_binaries(ow, bin, update_time);
                    }
                    ow.NewBinaries.Clear();
                }
            }
            Manager.TulipContext.SaveChanges();
        }

        private void update_analog(OutstationWrapper ow, IndexedValue<Analog> ana, DateTime update_time)
        {
            /* Check if the point currently exists in the points table */
            // TODO historical logging
            Point p = Manager.TulipContext.Points.SingleOrDefault(x => x.OutstationID == ow.Model.Id && x.PointIndex == ana.index && x.Type == POINT_TYPE.ANALOG_STATUS);
            DateTime epoch = new DateTime(1970, 1, 1);
            DateTime timestamp;

            if (ana.value.time == epoch)
            {
                timestamp = update_time;
            }
            else
            {
                timestamp = ana.value.time;
            }

            if (p == null)
            {
                p = new Point();
                p.OutstationID = ow.Model.Id;
                p.Type = POINT_TYPE.ANALOG_STATUS;
                p.PointIndex = (int) ana.index;
                p.ValueAnalog = Convert.ToSingle(ana.value.value);
                p.Status = POINT_STATUS.DETECTED;
                p.LastUpdate = update_time;
                p.LastMeasurement = timestamp;
                p.Quality = ana.value.quality;
                Manager.TulipContext.Points.Add(p);
            }
            else
            {
                // if a series of measurements come through, only keep the most recent
                if (timestamp > p.LastMeasurement)
                {
                    p.ValueAnalog = Convert.ToSingle(ana.value.value);
                    //p.Status = POINT_STATUS.
                    p.LastMeasurement = timestamp;
                    p.Quality = ana.value.quality;
                    p.LastUpdate = update_time;
                }
            }
            // (cw = Channels.First(x => x.Model.Id == m.ChannelID)
            
        }

        private void update_binaries(OutstationWrapper ow, IndexedValue<Binary> bin, DateTime update_time)
        {
            /* Check if the point currently exists in the points table */
            // TODO historical logging
            Point p = Manager.TulipContext.Points.SingleOrDefault(x => x.OutstationID == ow.Model.Id && x.PointIndex == bin.index && x.Type == POINT_TYPE.DIGITAL_STATUS);
            DateTime epoch = new DateTime(1970, 1, 1);
            DateTime timestamp;

            if (bin.value.time == epoch)
            {
                timestamp = update_time;
            }
            else
            {
                timestamp = bin.value.time;
            }

            if (p == null)
            {
                p = new Point();
                p.OutstationID = ow.Model.Id;
                p.Type = POINT_TYPE.DIGITAL_STATUS;
                p.PointIndex = (int)bin.index;
                p.ValueDigital = Convert.ToInt32(bin.value.value);
                p.Status = POINT_STATUS.DETECTED;
                
                p.LastUpdate = update_time;
                p.LastMeasurement = timestamp;

                
                
                p.Quality = bin.value.quality;
                Manager.TulipContext.Points.Add(p);
            }
            else
            {
                // if a series of measurements come through, only keep the most recent
                if (timestamp > p.LastMeasurement)
                {
                    p.ValueDigital = Convert.ToInt32(bin.value.value);
                    //p.Status = POINT_STATUS.
                    p.LastMeasurement = timestamp;
                    p.Quality = bin.value.quality;
                    p.LastUpdate = update_time;
                }
            }
            
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
