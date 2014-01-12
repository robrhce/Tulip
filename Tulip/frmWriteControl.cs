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
        public Command ReturnValue;

        private Point _point;

        public frmWriteControl(Command previous, Point Point)
        {
            InitializeComponent();
            _point = Point;


            //Point point = previous.Point;
            Outstation os = _point.Outstation;

            txtOutstationID.Text = _point.OutstationID.ToString();
            txtAddress.Text = _point.Outstation.Address.ToString();
            txtPointType.Text = _point.Type.ToString();
            txtPointIndex.Text = _point.PointIndex.ToString();


            comboDigOperation.DataSource = Enum.GetValues(typeof(ControlCode))
    .Cast<ControlCode>()
    .Select(p => new { Key = (ControlCode)p, Value = p.ToString() })
    .ToList();

            comboDigOperation.DisplayMember = "Value";
            comboDigOperation.ValueMember = "Key";
            
            
            if (previous != null)
            {

                if (_point.Type == POINT_TYPE.ANALOG_CONTROL)
                {
                    txtAnalogValue.Text = previous.AnalogValue.ToString();
                }
                else if (_point.Type == POINT_TYPE.DIGITAL_CONTROL)
                {
                    comboDigOperation.SelectedValue = previous.DigitalControl;
                    txtDigCount.Text = previous.DigitalCount.ToString();
                    txtDigOnTime.Text = previous.DigitalOnTime.ToString();
                    txtDigOffTime.Text = previous.DigitalOffTime.ToString();

                }
                else
                {
                    throw new ArgumentException();
                }
            }

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            Command c = new Command();

            if (_point.Type == POINT_TYPE.DIGITAL_CONTROL)
            {
                c.DigitalOffTime = Convert.ToInt32(txtDigOffTime.Text);
                c.DigitalOnTime = Convert.ToInt32(txtDigOnTime.Text);
                c.DigitalCount = Convert.ToInt32(txtDigCount.Text);
                c.DigitalControl = (ControlCode)comboDigOperation.SelectedValue;
            }
            else if (_point.Type == POINT_TYPE.ANALOG_CONTROL)
            {
                c.AnalogValue = Convert.ToSingle(txtAnalogValue.Text);
            }
            ReturnValue = c;
            this.Close();
        }
    }
}
