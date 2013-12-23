using DNP3.Interface;
using GUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tulip
{
    public class TextWriterLogAdapter : ILogHandler
    {
        private static TextWriterLogAdapter instance;// = new TextBoxLogAdapter();
        private TextWriter T;

        public static ILogHandler Instance
        {
            get
            {
                return instance;
            }
        }

        public void Log(LogEntry entry)
        {
            T.WriteLine(entry.message);
            //Console.WriteLine(DateTime.Now + " - " + entry.message);
        }

        public TextWriterLogAdapter(TextWriter t)
        {
            T = t;
        }

        private TextWriterLogAdapter()
        { }
    }
}
