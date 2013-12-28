using CommsLib.Util;
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

        public Action<ChannelWrapper> OnChannelStateChange;
        public Action<OutstationWrapper> OnOutstationStateChange;

        public IDNP3Manager DNP3Manager;

        public List<ChannelWrapper> Channels = new List<ChannelWrapper>();
        public List<OutstationWrapper> Outstations = new List<OutstationWrapper>();

        public StringBuilder Log = new StringBuilder();

        public Manager(IDNP3Manager DNP3Manager)
        {
            this.DNP3Manager = DNP3Manager;
            //this.Channels = new List<ChannelWrapper>();
        }

        public void AddChannels(List<Channel> channels)
        {
            foreach (Channel c in channels)
            {
                Dictionary<String, String> ConnectionOptions = StringToDictionary.Convert(c.ConnectionString);
                // TODO: Try/catch around this to continue after hitting a bad channel
                switch(c.Type.ToLower())
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
                            
                           // wr.Channel.AddStateListener(new Action<ChannelState>(wr.StateChanged));

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
                    if ((cw = Channels.First(x => x.Model.Id == m.ChannelID)) != null)
                    {
                        var config = new MasterStackConfig();
                        config.master.integrityPeriod = TimeSpan.FromSeconds(-1);
                        config.master.taskRetryPeriod = TimeSpan.FromSeconds(60);
                        config.link.localAddr = 30001;
                        config.link.remoteAddr = Convert.ToUInt16(o.Address);
                        config.link.timeout = TimeSpan.FromSeconds(10);
                        config.link.useConfirms = true; //setup your stack configuration here.
                        config.app.rspTimeout = TimeSpan.FromSeconds(50);

                        var master = cw.Channel.AddMaster("master", LogLevel.Interpret, PrintingMeasurementHandler.Instance, config);
                        OutstationWrapper OW = new OutstationWrapper(o, master);
                        OW.OnStateChanged += this.OutstationStateChanged;

                        master.AddStateListener(new Action<StackState>(OW.StateChanged));

                        var classMask = PointClassHelpers.GetMask(PointClass.PC_CLASS_1, PointClass.PC_CLASS_2, PointClass.PC_CLASS_3);
                        var classScan = master.AddClassScan(classMask, TimeSpan.FromSeconds(-1), TimeSpan.FromSeconds(-1));

                        Outstations.Add(OW);

                        master.Enable(); // enable communications
            
                    }
                }


            }
        }

        public void OutstationStateChanged()
        {
            // TODO: FIX
            if (OnOutstationStateChange != null)
                OnOutstationStateChange(null);
        }

    }
}
