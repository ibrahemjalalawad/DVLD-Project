using BussinessLayesDVLD;
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
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
        }
        
        int _UserID;
        clsUser _User;
        public int UserID { get { return _UserID; } }
      

        private void _FillUserInfo()
        {
            ctrlPersonDetails1.LoadPersonInfo(_User.PersonID);
            lblUserName.Text = _User.UserName.ToString();
            lblUserID.Text=_User.UserID.ToString();

            if (_User.isActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
        }
        private void _refreshDefaultValues()
        {
            ctrlPersonDetails1._ResetPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.Find(_UserID);
            if(_User == null)
            {
                _refreshDefaultValues();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();

        }


        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {
          
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
