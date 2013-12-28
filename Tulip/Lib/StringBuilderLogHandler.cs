using DNP3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulip.Lib
{
    class StringBuilderLogHandler : ILogHandler
    {
        private static readonly StringBuilderLogHandler instance = new StringBuilderLogHandler();

        public StringBuilder SB = new StringBuilder();

        public static ILogHandler Instance
        {
            get
            {
                return instance;
            }
        }

        public void Log(LogEntry entry)
        {
            SB.AppendLine(DateTime.Now + " - " + entry.message);
        }

        private StringBuilderLogHandler()
        {
        }
    }
}
