using DNP3.Interface;
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
    public partial class frmPointHistory : Form
    {
        private Lib.Manager _manager;

        private Outstation _o;
        private Point _p;

        public frmPointHistory(Lib.Manager Manager, Outstation O, Point P)
        {
            InitializeComponent();

            _manager = Manager;

            _o = O;
            _p = P;
        }

        private void frmPointHistory_Load(object sender, EventArgs e)
        {
            var points = _manager.TulipContext.Points.Where(X => X.OutstationID == _o.Id && (X.Type == BasicType.DIGITAL_STATUS || X.Type == BasicType.ANALOG_STATUS));
            this.pointBindingSource.DataSource = new BindingList<Point>(points.ToList());//.ToBindingList();// _manager.TulipContext.Points.Local.ToBindingList();

        }

        private void pointDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_p != null)
            {
                Point found = pointBindingSource.List.OfType<Point>().SingleOrDefault(x => x.Id == _p.Id);
                if (found != null)
                {
                    int pos = pointBindingSource.IndexOf(found);
                    pointBindingSource.Position = pos;
                }

                //pointBindingSource.Find("Id", _p.Id);  //pointBindingSource.List.OfType<Point>().FirstOrDefault(x => x.Id == _p.Id).   // ("ID", _p.Id);
            }
        }

        private void historiesDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            History h = (History)dgvHistory.Rows[e.RowIndex].DataBoundItem;

            // get the currently selected point
            // TODO: change to query??
            if (dgvPoints.SelectedRows.Count > 0)
            {

                Point p = (Point)dgvPoints.SelectedRows[0].DataBoundItem;

                switch (dgvHistory.Columns[e.ColumnIndex].Name)
                {
                    case "dgvHistory_colValue":

                        switch (p.Type)
                        {
                            case BasicType.ANALOG_STATUS:
                                if (h.ValueAnalog.HasValue)
                                    e.Value = h.ValueAnalog.ToString();
                                else
                                    e.Value = "Unknown";

                                break;

                            case BasicType.DIGITAL_STATUS:
                                if (h.ValueDigital.HasValue)
                                    e.Value = h.ValueDigital.ToString();
                                else
                                    e.Value = "Unknown";

                                break;

                            default:

                                break;
                        }

                        break;

                }
            }

        }
    }
}
