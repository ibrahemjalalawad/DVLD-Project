﻿using BussinessLayesDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        private void _RefreshApplicationTypesList()
        {
            dgvApplicationTypes.DataSource = clsApplicationTypes.GetAllApplicationTypes();
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();
        }
        private void _Load()
        {
            _RefreshApplicationTypesList();
            lblRecordsCount.Text = dgvApplicationTypes.Rows.Count.ToString();
            if(dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 400;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;
            }
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }
    }
}
