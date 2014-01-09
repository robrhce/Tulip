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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            writeControlToolStripMenuItem.Enabled = false;

            if (pointDataGridView.SelectedRows.Count == 1)
            {
                if (pointDataGridView.SelectedRows[0].DataBoundItem is Point)
                {
                    Point P = (Point)pointDataGridView.SelectedRows[0].DataBoundItem;
                    if (P.Type == POINT_TYPE.DIGITAL_CONTROL || P.Type == POINT_TYPE.ANALOG_CONTROL)
                        writeControlToolStripMenuItem.Enabled = true;
                }
            }

        }

        private void writeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pointDataGridView.SelectedRows.Count == 1)
            {
                if (pointDataGridView.SelectedRows[0].DataBoundItem is Point)
                {
                    Point P = (Point)pointDataGridView.SelectedRows[0].DataBoundItem;
                    if (P.Type == POINT_TYPE.DIGITAL_CONTROL || P.Type == POINT_TYPE.ANALOG_CONTROL)
                    {
                        frmWriteControl fwc = new frmWriteControl(P, Context);
                        fwc.ShowDialog();

                        if (fwc.ReturnValue != null)
                        {
                            Command newCommand = fwc.ReturnValue;
                            P.Commands.Add(newCommand);
                            //Context.Commands.Add(newCommand);
                            
                            // TODO: Concurrency??

                            Context.SaveChanges();

                            Main.Manager.PostCommand(newCommand);
                                
                        }
                    }
                }
            }
        }

    }
}
