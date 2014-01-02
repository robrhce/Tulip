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
        Tulip.TulipEntities _context;

        public Main()
        {
            InitializeComponent();

            //TextBoxTextWriter tx = new TextBoxTextWriter(txtLog, System.Threading.Thread.CurrentThread);
            //Console.SetOut(tx);
            
            
             _context = new TulipEntities();

            
            IDNP3Manager mgr = DNP3ManagerFactory.CreateManager();
            _manager = new Lib.Manager(mgr);
            
            StringBuilderLogHandler SBLH = (StringBuilderLogHandler) StringBuilderLogHandler.Instance;
            SBLH.SB = _manager.Log;

            mgr.AddLogHandler(SBLH); //this is optional
            
             

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
            this.BeginInvoke(new Action(refresh_outstations2));
        }

        private void refresh_outstations2()//OutstationWrapper updated)
        {
            List<OutstationWrapper> cws = _manager.Outstations;

            tblOutstationStatus_Model.Rows.Clear();
            foreach (OutstationWrapper C in cws)
            {

                tblOutstationStatus_Model.Rows.Add(new XPTable.Models.Row(new[] { "", C.Model.Id.ToString(), C.Model.Name, C.state.ToString() }));
            }
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

            var Channels = from os in _context.Channels
                           select os;

            var Outstations = from os in _context.Outstations
                              select os;

            List<Channel> CC = Channels.ToList();

            // TODO: DONT START STUFF UNTIL WINDOW HANDLE HAS BEEN CREATED OR WE WILL LOCK SHIT UP


            _manager.AddChannels(Channels.ToList());
            _manager.OnChannelStateChange += this.refresh_channels;

            _manager.AddOutstations(Outstations.ToList());
            _manager.OnOutstationStateChange += this.refresh_outstations;

            _manager.OnOutstationMeasurementReceived += this.new_measurement_handler;

            refresh_outstations2();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmPointSummary frmPS = new frmPointSummary(_context);
            frmPS.Show();
        }

        private void new_measurement_handler()
        {
            DateTime update_time = DateTime.UtcNow;
            foreach (OutstationWrapper ow in _manager.Outstations)
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
            _context.SaveChanges();
        }

        private void update_analog(OutstationWrapper ow, IndexedValue<Analog> ana, DateTime update_time)
        {
            /* Check if the point currently exists in the points table */
            // TODO historical logging
            Point p = _context.Points.SingleOrDefault(x => x.OutstationID == ow.Model.Id && x.PointIndex == ana.index && x.Type == POINT_TYPE.ANALOG_STATUS);
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
                p.ValueFloat = Convert.ToSingle(ana.value.value);
                p.Status = POINT_STATUS.DETECTED;
                p.LastUpdate = update_time;
                p.LastMeasurement = timestamp;
                p.Quality = ana.value.quality;
                _context.Points.Add(p);
            }
            else
            {
                // if a series of measurements come through, only keep the most recent
                if (timestamp > p.LastMeasurement)
                {
                    p.ValueFloat = Convert.ToSingle(ana.value.value);
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
            Point p = _context.Points.SingleOrDefault(x => x.OutstationID == ow.Model.Id && x.PointIndex == bin.index && x.Type == POINT_TYPE.DIGITAL_STATUS);
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
                _context.Points.Add(p);
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
    }
}
