﻿using CommsLib.Util;
using DNP3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulip.Lib
{
    public class Manager
    {
        public TulipEntities TulipContext;

        public Action<ChannelWrapper> OnChannelStateChange;
        public Action<OutstationWrapper> OnOutstationStateChange;
        public Action OnOutstationMeasurementReceived;

        public IDNP3Manager DNP3Manager;

        public List<ChannelWrapper> Channels = new List<ChannelWrapper>();
        public List<OutstationWrapper> Outstations = new List<OutstationWrapper>();

        public StringBuilder Log = new StringBuilder();

        public Manager(IDNP3Manager DNP3Manager)
        {
            this.DNP3Manager = DNP3Manager;
            TulipContext = new TulipEntities();
            //this.Channels = new List<ChannelWrapper>();
        }

        public void AddChannels(List<Channel> channels)
        {
            foreach (Channel c in channels.Where(x => x.Enabled > 0))
            {
                Dictionary<String, String> ConnectionOptions = StringToDictionary.Convert(c.ConnectionString);
                // TODO: Try/catch around this to continue after hitting a bad channel
                switch (c.Type.ToLower())
                {
                    case "tcpclient":

                        if (ConnectionOptions.Keys.Contains("address") && ConnectionOptions.Keys.Contains("port"))
                        {
                            UInt16 port = Convert.ToUInt16(ConnectionOptions["port"]);
                            if (port == 0) throw new FormatException();

                            // TODO timespan option
                            IChannel ch = DNP3Manager.AddTCPClient(c.Name, LogLevel.Info, TimeSpan.FromSeconds(5), ConnectionOptions["address"], port);
                            ChannelWrapper wr = new ChannelWrapper(c, ch);
                            wr.OnStateChanged += this.ChannelStateChanged;

                            wr.Channel.AddStateListener(new Action<ChannelState>(wr.StateChanged));

                            Channels.Add(wr);

                        }

                        break;


                    case "serial":
                        if (ConnectionOptions.Keys.Contains("port") && ConnectionOptions.Keys.Contains("baud"))
                        {
                            UInt16 baud = Convert.ToUInt16(ConnectionOptions["baud"]);
                            if (baud == 0) throw new FormatException();

                            // TODO: more serial settings
                            SerialSettings ss = new SerialSettings(ConnectionOptions["port"], baud, 8, 1, Parity.NONE, FlowControl.NONE);
                            // TODO: timespan option
                            IChannel ch = DNP3Manager.AddSerial(c.Name, LogLevel.Info, TimeSpan.FromSeconds(5), ss);
                            ChannelWrapper wr = new ChannelWrapper(c, ch);
                            wr.OnStateChanged += this.ChannelStateChanged;

                            wr.Channel.AddStateListener(new Action<ChannelState>(wr.StateChanged));



                            Channels.Add(wr);

                        }

                        break;
                }
            }
        }

        private void ChannelStateChanged()
        {
            // TODO: FIX
            if (OnChannelStateChange != null)
                OnChannelStateChange(null);
        }

        public void AddOutstations(List<Outstation> outstations)
        {
            foreach (Outstation o in outstations)
            {
                if (o.OutstationChannelMappings.Count > 0)
                {
                    // Only load the first mapping
                    OutstationChannelMapping m = o.OutstationChannelMappings[0];


                    // Select the first (and should be only) channel that matches the mapping
                    ChannelWrapper cw = null;
                    if ((cw = Channels.SingleOrDefault(x => x.Model.Id == m.ChannelID)) != null)
                    {
                        var config = new MasterStackConfig();
                        config.master.integrityPeriod = TimeSpan.FromSeconds(60);
                        config.master.taskRetryPeriod = TimeSpan.FromSeconds(60);
                        config.link.localAddr = 30001;
                        config.link.remoteAddr = Convert.ToUInt16(o.Address);
                        config.link.timeout = TimeSpan.FromSeconds(10);
                        config.link.useConfirms = true; //setup your stack configuration here.
                        config.app.rspTimeout = TimeSpan.FromSeconds(50);

                        OutstationWrapper OW = new OutstationWrapper(o);
                        var master = cw.Channel.AddMaster("master", LogLevel.Debug, OW, config);
                        OW.Master = master;
                        OW.OnStateChanged += this.Callback_OutstationStateChanged;
                        OW.OnMeasurementsReceived += this.Callback_OutstationMeasurementReceived;

                        master.AddStateListener(new Action<StackState>(OW.StateChanged));

                        var classMask = PointClassHelpers.GetMask(PointClass.PC_CLASS_1, PointClass.PC_CLASS_2, PointClass.PC_CLASS_3);
                        var classScan = master.AddClassScan(classMask, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(60));

                        Outstations.Add(OW);

                        master.Enable(); // enable communications

                    }
                }
            }
        }

        public void Callback_OutstationStateChanged()
        {
            // TODO: FIX
            if (OnOutstationStateChange != null)
                OnOutstationStateChange(null);
        }

        public void Callback_OutstationMeasurementReceived(OutstationWrapper ow, IMeasurementUpdate update)
        {
            // TODO make async

            DateTime update_time = DateTime.UtcNow;
            
            // TODO: FIX 

            if (update.AnalogUpdates.Count > 0)
            {
                foreach (IndexedValue<Analog> ana in update.AnalogUpdates)
                {
                    // TODO: better way to do this?
                    IndexedValue<DataPoint> dp = new IndexedValue<DataPoint>(ana.value, ana.index);
                    update_point_master(ow, dp, update_time);
                    update_point_history(ow, dp, update_time);
                }
            }

            if (update.BinaryUpdates.Count > 0)
            {
                foreach (IndexedValue<Binary> bin in update.BinaryUpdates)
                {
                    IndexedValue<DataPoint> dp = new IndexedValue<DataPoint>(bin.value, bin.index);
                    update_point_master(ow, dp, update_time);
                    update_point_history(ow, dp, update_time);
                }
            }

            if (OnOutstationMeasurementReceived != null)
                OnOutstationMeasurementReceived();

            TulipContext.SaveChanges();
        }

        public void PostCommand(Point p, Command unatt_c)
        {


            OutstationWrapper ow = Outstations.Where(x => x.Model.Id == p.OutstationID).SingleOrDefault();


            if (ow != null)
            {
                if (ow.state == StackState.COMMS_UP)
                {
                    unatt_c.TimestampSent = DateTime.UtcNow;


                    if (p.Type == BasicType.ANALOG_CONTROL)
                    {
                        AnalogOutputFloat32 aof = unatt_c.GetAnalogOutput();

                        var future = ow.Master.GetCommandProcessor().DirectOperate(aof, Convert.ToUInt32(p.PointIndex));
                        // Use a lambda to curry the command object into the callback as well 
                        future.Listen((cr) => CommandComplete(unatt_c, cr));

                    }
                    else if (p.Type == BasicType.DIGITAL_CONTROL)
                    {
                        ControlRelayOutputBlock crob = unatt_c.GetCROB();

                        var future = ow.Master.GetCommandProcessor().DirectOperate(crob, Convert.ToUInt32(p.PointIndex));
                        // Use a lambda to curry the command object into the callback as well 
                        future.Listen((cr) => CommandComplete(unatt_c, cr));
                    }
                    else
                    {
                        throw new ArgumentException("c.Point.Type != POINT_TYPE.{DIGITAL_CONTROL,ANALOG_CONTROL}");
                    }

                    // TODO: how to ensure this makes it into the DB before the callback gets fired?
                    // or does it not matter because the first line pushes it into the model?

                    p.Commands.Add(unatt_c);
                    TulipContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Cannot issue command, communications with outstation down (COMMS_DOWN)");
                }
            }
        }

        public void CommandComplete(Command c, CommandResponse cr)
        {
            c.Response = cr.Status;
            c.Result = cr.Result;
            c.TimestampResponse = DateTime.UtcNow;

            TulipContext.SaveChanges();

            switch (cr.Result)
            {
                case CommandResult.RESPONSE_OK:

                    break;

                case CommandResult.NO_COMMS:
                    // set point state

                    break;

                case CommandResult.TIMEOUT:
                    // set point state
                    break;
            }

            // TODO: set point state for different results
        }

        private void update_point_master(OutstationWrapper ow, IndexedValue<DataPoint> point, DateTime update_time)
        {
            DateTime timestamp;

            if (point.value.time == TimeStamp.epoch)
                timestamp = update_time;
            else
                timestamp = point.value.time;

            /* Check if the point currently exists in the points table */
            Point point_model = ow.Model.Points.SingleOrDefault(x => x.PointIndex == point.index && x.Type == point.value.GetBasicType());

            /* Create a new point */
            if (point_model == null)
            {
                point_model = new Point();
                point_model.OutstationID = ow.Model.Id;
                point_model.PointIndex = (int)point.index;
                point_model.Status = POINT_STATUS.DETECTED;
                point_model.Type = point.value.GetBasicType();

                TulipContext.Points.Add(point_model);
            }
            else
            {
                // ?? p.Status == POINT_STATUS.UPDATED;
            }

            // if a series of measurements come through, only keep the most recent
            if (timestamp > point_model.LastMeasurement)
            {
                if (point_model.Type == BasicType.ANALOG_STATUS)
                    point_model.ValueAnalog = point.value.ToSingle();
                else
                    point_model.ValueDigital = Convert.ToInt32(point.value.ToBoolean());

                point_model.LastMeasurement = timestamp;
                point_model.Quality = point.value.quality;
                point_model.LastUpdate = update_time;
            }
        }

        private void update_point_history(OutstationWrapper ow, IndexedValue<DataPoint> point, DateTime update_time)
        {
            History H = new History();
            

            // get the master point
            BasicType b = point.value.GetBasicType();

            Point P = TulipContext.Points.SingleOrDefault(X => X.PointIndex == point.index && X.Type == b);

            if (P != null)
            {
                DateTime timestamp;
                
                if (point.value.time == TimeStamp.epoch)
                    timestamp = update_time;
                else
                    timestamp = point.value.time;

                H.Timestamp = timestamp;
                H.Quality = point.value.quality;

                if (P.Type == BasicType.ANALOG_STATUS)
                    H.ValueAnalog = point.value.ToSingle();
                else
                    H.ValueDigital = Convert.ToInt32(point.value.ToBoolean());

                P.Histories.Add(H);
            }
        }

        private void new_measurement_handler()
        {
            // TODO: FIX

            DateTime update_time = DateTime.UtcNow;
            /*
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
             */
        }
    }
}
