using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DNP3.Adapter;
using DNP3.Interface;

namespace TestSlave
{
    class Program
    {
        static void Main(string[] args)
        {
            IDNP3Manager mgr = DNP3ManagerFactory.CreateManager();
            mgr.AddLogHandler(PrintingLogAdapter.Instance); //this is optional
            var channel = mgr.AddTCPClient("server", LogLevel.Interpret, TimeSpan.FromSeconds(5), "127.0.0.1", 8888);

            //optionally, add a listener for the channel state
            channel.AddStateListener(state => Console.WriteLine("Server state: " + state));

            var config = new SlaveStackConfig();
            config.link.localAddr = 555;
            config.link.remoteAddr = 30001;


            var outstation = channel.AddOutstation("outstation", LogLevel.Info, RejectingCommandHandler.Instance, PrintingTimeWriteHandler.Instance, config);

            
            //optionally, add a listener for the stack state
            outstation.AddStateListener(state => Console.WriteLine("Outstation state: " + state));

            outstation.Enable(); // enable communications

            Console.WriteLine("Press <Enter> to randomly change a value");
            var publisher = outstation.GetDataObserver();
            Random r = new Random();
            while (true)
            {
                Console.ReadLine();
                int value = r.Next(UInt16.MaxValue);
                System.Console.WriteLine("Change Analog 0 to: " + value);
                publisher.Start();
                publisher.Update(new Analog(value, 1, DateTime.Now), 0);
                publisher.End();
            }
        }
    }
}
