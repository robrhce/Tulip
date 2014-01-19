using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System;
using DNP3.Interface;

namespace Tulip
{
    public partial class frmPointCommandHistory : Form
    {

        private Lib.Manager _manager;

        private Outstation _o;
        private Point _p;

        public frmPointCommandHistory(Lib.Manager Manager, Outstation O, Point P)
        {
            InitializeComponent();
            _manager = Manager;

            _o = O;
            _p = P;
        }

        private void frmPointCommandHistory_Load(object sender, EventArgs e)
        {

            var points = _manager.TulipContext.Points.Where(X => X.OutstationID == _o.Id && (X.Type == BasicType.DIGITAL_CONTROL || X.Type == BasicType.ANALOG_CONTROL));
            this.pointBindingSource.DataSource = new BindingList<Point>(points.ToList());//.ToBindingList();// _manager.TulipContext.Points.Local.ToBindingList();
            /*
            dgvCommand_colResponse.DataSource = Enum.GetValues(typeof(CommandStatus))
                .Cast<CommandStatus>()
                .Select(p => new { Key = (CommandStatus)p, Value = p.ToString() })
                .ToList();

            dgvCommand_colResponse.DisplayMember = "Value";
            dgvCommand_colResponse.ValueMember = "Key";
            dgvCommand_colResponse.ValueType = typeof(CommandStatus);

            dgvCommand_colResult.DataSource = Enum.GetValues(typeof(CommandResult))
                .Cast<CommandResult>()
                .Select(p => new { Key = (CommandResult)p, Value = p.ToString() })
                .ToList();

            dgvCommand_colResult.DisplayMember = "Value";
            dgvCommand_colResult.ValueMember = "Key";
            dgvCommand_colResult.ValueType = typeof(CommandResult);
            */


        }

        private void pointDataGridView_DataBindingComplete(object sender, System.Windows.Forms.DataGridViewBindingCompleteEventArgs e)
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

        private void commandDataGridView_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            Command c = (Command)dgvCommands.Rows[e.RowIndex].DataBoundItem;

            // get the currently selected point
            Point p = (Point)dgvPoints.SelectedRows[0].DataBoundItem;

            switch (dgvCommands.Columns[e.ColumnIndex].Name)
            {
                case "dgvCommands_colValue":

                    switch (p.Type)
                    {
                        case BasicType.ANALOG_CONTROL:
                            if (c.AnalogValue.HasValue)
                                e.Value = c.AnalogValue.ToString();
                            else
                                e.Value = "Unknown";

                            break;

                        case BasicType.DIGITAL_CONTROL:
                            if (c.DigitalControl.HasValue)
                                e.Value = c.DigitalControl.ToString();
                            else
                                e.Value = "Unknown";

                            break;

                        default:

                            break;
                    }
                    
                    break;

                case "dgvCommands_colResponse":
                    e.Value = c.Response.ToString();
                    break;

                case "dgvCommands_colResult":
                    e.Value = c.Response.ToString();
                    break;

            }

        }

        private void dgvCommands_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }
    }
}
