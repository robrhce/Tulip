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
using DNP3.Interface;

namespace Tulip
{
    public partial class frmPointSummary : Form
    {
        private Lib.Manager _manager;

        public frmPointSummary(Lib.Manager Manager)
        {
            InitializeComponent();
            this._manager = Manager;
        }

        private void frmPointSummary_Load(object sender, EventArgs e)
        {
            //Context.Outstations.Load();
            //_context.Points.Load();
            this.outstationBindingSource.DataSource = _manager.TulipContext.Outstations.Local.ToBindingList();
            //this.pointBindingSource.DataSource = _context.Points.Local.ToBindingList();

        }

        private void tmrRefreshPoints_Tick(object sender, EventArgs e)
        {
            //_context.Outstations.Load();
            this.dgvPoints.SuspendLayout();
            this.dgvPoints.Refresh();
            this.dgvPoints.ResumeLayout();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            writeControlToolStripMenuItem.Enabled = false;

            if (dgvPoints.SelectedRows.Count == 1)
            {
                if (dgvPoints.SelectedRows[0].DataBoundItem is Point)
                {
                    Point P = (Point)dgvPoints.SelectedRows[0].DataBoundItem;
                    if (P.Type == BasicType.DIGITAL_CONTROL || P.Type == BasicType.ANALOG_CONTROL)
                    {
                        writeControlToolStripMenuItem.Enabled = true;
                        pointHistoryToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        writeControlToolStripMenuItem.Enabled = false;
                        pointHistoryToolStripMenuItem.Enabled = true;
                    }

                }
            }

        }

        private void writeControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPoints.SelectedRows.Count == 1)
            {
                if (dgvPoints.SelectedRows[0].DataBoundItem is Point)
                {
                    Point P = (Point)dgvPoints.SelectedRows[0].DataBoundItem;
                    if (P.Type == BasicType.DIGITAL_CONTROL || P.Type == BasicType.ANALOG_CONTROL)
                    {
                        /* query for the most recent command for that point and pre-fill the values (if it exists) */
                        Command lastCommand = _manager.TulipContext.Commands
                   .Where(x => x.PointID == P.Id)
                   .OrderByDescending(d => d.TimestampSent)
                   .FirstOrDefault();

                        frmWriteControl fwc = new frmWriteControl(lastCommand, P);
                        fwc.ShowDialog();

                        if (fwc.ReturnValue != null)
                        {
                            Command newCommand = fwc.ReturnValue;


                            // TODO: push this into manager ?
                            //P.Commands.Add(newCommand);
                            //Context.Commands.Add(newCommand);

                            // TODO: Concurrency??

                            //_context.SaveChanges();

                            Main.Manager.PostCommand(P, newCommand);

                        }
                    }
                }
            }
        }

        private void outstationDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }

        private void commandHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPoints.SelectedRows.Count == 1)
            {
                if (dgvPoints.SelectedRows[0].DataBoundItem is Point)
                {
                    Point P = (Point)dgvPoints.SelectedRows[0].DataBoundItem;
                    if (P.Type == BasicType.DIGITAL_CONTROL || P.Type == BasicType.ANALOG_CONTROL)
                    {
                        new frmPointCommandHistory(_manager, P.Outstation, P).Show();
                    }
                }
            }
        }

        private void pointDataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvPoints_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Point p = (Point)dgvPoints.Rows[e.RowIndex].DataBoundItem;

            switch (p.Type)
            {
                case BasicType.ANALOG_STATUS:
                    if (p.ValueAnalog.HasValue)
                        dgvPoints["dgvPoints_colValue", e.RowIndex].Value = p.ValueAnalog.ToString();
                    else
                        dgvPoints["dgvPoints_colValue", e.RowIndex].Value = "Unknown";

                    break;

                case BasicType.DIGITAL_STATUS:
                    if (p.ValueDigital.HasValue)
                        dgvPoints["dgvPoints_colValue", e.RowIndex].Value = p.ValueDigital > 0 ? "ON" : "OFF";
                    else
                        dgvPoints["dgvPoints_colValue", e.RowIndex].Value = "Unknown";

                    break;

                default:

                    break;
            }
        }

        private void pointHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPoints.SelectedRows.Count == 1)
            {
                if (dgvPoints.SelectedRows[0].DataBoundItem is Point)
                {
                    Point P = (Point)dgvPoints.SelectedRows[0].DataBoundItem;
                    if (P.Type == BasicType.DIGITAL_STATUS || P.Type == BasicType.ANALOG_STATUS)
                    {
                        new frmPointHistory(_manager, P.Outstation, P).Show();
                    }
                }
            }
        }

    }
}
