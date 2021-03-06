﻿using System;
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
    public partial class frmPointConfiguration : Form
    {
        private TulipEntities _context;
        public frmPointConfiguration(TulipEntities Context)
        {
            InitializeComponent();
            this._context = Context;

            dataGridViewTextBoxColumn7.DataSource = Enum.GetValues(typeof(BasicType))
    .Cast<BasicType>()
    .Select(p => new { Key = (BasicType)p, Value = p.ToString() })
    .ToList();

            dataGridViewTextBoxColumn7.DisplayMember = "Value";
            dataGridViewTextBoxColumn7.ValueMember = "Key";
            dataGridViewTextBoxColumn7.ValueType = typeof(BasicType);


        }

        private void frmPointSummary_Load(object sender, EventArgs e)
        {
            //Context.Outstations.Load();
            //_context.Points.Load();
            this.outstationBindingSource.DataSource = _context.Outstations.Local.ToBindingList();
            //this.pointBindingSource.DataSource = _context.Points.Local.ToBindingList();

        }

        private void tmrRefreshPoints_Tick(object sender, EventArgs e)
        {
            //_context.Outstations.Load();
            this.pointDataGridView.SuspendLayout();
            this.pointDataGridView.Refresh();
            this.pointDataGridView.ResumeLayout();
        }

        private void outstationBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {



            this.Validate();

            // Currently, the Entity Framework doesn’t mark the entities 
            // that are removed from a navigation property (in our example the Products)
            // as deleted in the context. 
            // The following code uses LINQ to Objects against the Local collection 
            // to find all products and marks any that do not have
            // a Category reference as deleted. 
            // The ToList call is required because otherwise 
            // the collection will be modified 
            // by the Remove call while it is being enumerated. 
            // In most other situations you can do LINQ to Objects directly 
            // against the Local property without using ToList first.

            foreach (var ocm in _context.Points.Local.ToList())
            {
                if (ocm.OutstationID == null || ocm.Outstation == null)
                {
                    _context.Points.Remove(ocm);
                }
            }

            // Save the changes to the database.
            this._context.SaveChanges();

            // Refresh the controls to show the values         
            // that were generated by the database.
            //this.outstationDataGridView.Refresh();
            this.pointDataGridView.Refresh();
        }

        private void pointDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
