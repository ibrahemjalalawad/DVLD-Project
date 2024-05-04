using BussinessLayesDVLD;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class frmManageUser : Form
    {
        public frmManageUser()
        {
            InitializeComponent();
        }

        private static DataTable _dtAllUsers;
        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
           dgvUsers.DataSource=_dtAllUsers;  
            lblUsersRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void _Load()
        {
            _RefreshUsersList();   
            cbFilterUsers.SelectedIndex = 0;
            lblNumberOFRecord.Text = dgvUsers.Rows.Count.ToString();
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;

            }
           


        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.ShowDialog();
        }

        private void frmManageUser_Load(object sender, EventArgs e)
        {
            _Load();
    

        }

        private void tbFilterUsers_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterUsers.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }
            if (tbFilterUsers.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblUsersRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with integer not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, tbFilterUsers.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, tbFilterUsers.Text.Trim());
            lblUsersRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbFilterUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterUsers.Text == "Is Active")
            {
                tbFilterUsers.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                tbFilterUsers.Visible = (cbFilterUsers.Text != "None");
                cbIsActive.Visible = false;
                if (cbFilterUsers.Text == "None")
                {
                   tbFilterUsers.Enabled = false;
                }
                else
                    tbFilterUsers.Enabled = true;

               
                    tbFilterUsers.Text = "";
                    tbFilterUsers.Focus();
               
            }
        }


        private void csmAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUserInfo();
            frm.ShowDialog();

           
            frmManageUser_Load(null, null);
        }

        private void tbFilterUsers_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase User id is selected.
            if (cbFilterUsers.Text == "User ID" || cbFilterUsers.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

          
            frmManageUser_Load(null, null);
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            Form frm = new frmShowUsersInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cmsEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature not implemented yet");
        }

        private void cmsPhone_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this feature not implemented yet");
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "ALL":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "ALL")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblUsersRecords.Text = _dtAllUsers.Rows.Count.ToString();

        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (clsUser._DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmManageUser_Load(null, null);
            }

            else
                MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmChangePassword Frm1 = new frmChangePassword(UserID);
            Frm1.ShowDialog();
        }

        private void cmsShowDetalis_Click(object sender, EventArgs e)
        {
            frmShowUsersInfo frm = new frmShowUsersInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }

   
    }

