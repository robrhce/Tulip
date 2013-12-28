using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tulip
{
    public partial class frmLog : Form
    {
        public frmLog(String Log)
        {
            InitializeComponent();
            txtLog.Text = Log;
        }
    }
}
