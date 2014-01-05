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
    public partial class frmWriteControl : Form
    {
        Point _p;
        TulipEntities _context;
        public Command ReturnValue;

        public frmWriteControl(Point point, TulipEntities context)
        {
            InitializeComponent();

            this._context = context;

            txtOutstationID.Text = point.OutstationID.ToString();
            txtAddress.Text = point.Outstation.Address.ToString();
            txtPointType.Text = point.Type.ToString();
            txtPointIndex.Text = point.PointIndex.ToString();


            comboDigOperation.DataSource = Enum.GetValues(typeof(ControlCode))
    .Cast<ControlCode>()
    .Select(p => new { Key = (ControlCode)p, Value = p.ToString() })
    .ToList();

            comboDigOperation.DisplayMember = "Value";
            comboDigOperation.ValueMember = "Key";

            /* query for the most recent command for that point and pre-fill the values (if it exists) */

            Command lastCommand = context.Commands
                .Where(x => x.PointID == point.Id)
                .OrderByDescending(d => d.TimestampSent)
                .FirstOrDefault();

            if (lastCommand != null)
            {
                if (point.Type == POINT_TYPE.ANALOG_CONTROL)
                {
                    txtAnalogValue.Text = lastCommand.AnalogValue.ToString();
                }
                else if (point.Type == POINT_TYPE.DIGITAL_CONTROL)
                {
                    comboDigOperation.SelectedValue = lastCommand.DigitalControl;
                    txtDigCount.Text = lastCommand.DigitalCount.ToString();
                    txtDigOnTime.Text = lastCommand.DigitalOnTime.ToString();
                    txtDigOffTime.Text = lastCommand.DigitalOffTime.ToString();
                }
            }

            _p = point;

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            Command c = new Command();

            if (_p.Type == POINT_TYPE.DIGITAL_CONTROL)
            {
                c.DigitalOffTime = Convert.ToInt32(txtDigOffTime.Text);
                c.DigitalOnTime = Convert.ToInt32(txtDigOnTime.Text);
                c.DigitalCount = Convert.ToInt32(txtDigCount.Text);
                c.DigitalControl = (ControlCode) comboDigOperation.SelectedValue;
            }
            else if (_p.Type == POINT_TYPE.ANALOG_CONTROL)
            {
                c.AnalogValue = Convert.ToSingle(txtAnalogValue.Text);
            }
            ReturnValue = c;
            this.Close();
        }
    }
}
