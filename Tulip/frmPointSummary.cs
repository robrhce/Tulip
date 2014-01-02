using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Tulip
{
    public partial class frmPointSummary : Form
    {
        private TulipEntities Context;
        public frmPointSummary(TulipEntities Context)
        {
            InitializeComponent();
            this.Context = Context;
        }

        private void frmPointSummary_Load(object sender, EventArgs e)
        {
            //Context.Outstations.Load();
            //_context.Points.Load();
            this.outstationBindingSource.DataSource = Context.Outstations.Local.ToBindingList();
            //this.pointBindingSource.DataSource = _context.Points.Local.ToBindingList();
            
        }

        private void tmrRefreshPoints_Tick(object sender, EventArgs e)
        {
            //_context.Outstations.Load();
            this.pointDataGridView.SuspendLayout();
            this.pointDataGridView.Refresh();
            this.pointDataGridView.ResumeLayout();
        }

    }
}
