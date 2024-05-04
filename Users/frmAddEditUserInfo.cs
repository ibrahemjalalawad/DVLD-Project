﻿using BussinessLayesDVLD;
using DVLD_Project.Global_Classes;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class frmAddEditUserInfo : Form
    {
        public frmAddEditUserInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddUser;
        }
        public frmAddEditUserInfo(int UserID)
        {

            InitializeComponent();
           _UserID = UserID;
            _Mode = enMode.UpdateUser;


        }
        
        public enum enMode
        { UpdateUser = 0, AddUser = 1 }
        public enum enGender { Male = 0, Female = 1 };

        private enMode _Mode;
        private int _UserID = -1;
        clsUser _User;
        int _SelectedPersonID = -1;


        private void _Load()
        {



            if (_Mode == enMode.AddUser)
            {
                label1.Text = "Add New User";
                _User = new clsUser();
               // ctrlPersonDeatailsWithFilter1.FilterFocus();
                tpLoginInfo.Enabled = false;
                return;

            }

            else
            {

                label1.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;

         
        }

        private void _LoadData()
        {
            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblUserID.Text = _UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.isActive;
            ctrlPersonDeatailsWithFilter1.LoadPersonInfo(_User.PersonID);


        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditUserInfo_Load(object sender, EventArgs e)
        {
            _Load();

            if (_Mode == enMode.UpdateUser)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _User.PersonID = ctrlPersonDeatailsWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.isActive = chkIsActive.Checked;


            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                //change form mode to update.
                _Mode = enMode.UpdateUser;
                label1.Text = "Update User";
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            };

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "UserName cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
            if (_Mode == enMode.AddUser)
            {

                if (clsUser.isUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.isUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }
            }
        

    }

        private void btnPersonInfoNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.UpdateUser)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUser.SelectedTab = tcUser.TabPages["tpLoginInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonDeatailsWithFilter1.PersonID != -1)
            {

                if (clsUser.isUserExistForPersonID(ctrlPersonDeatailsWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonDeatailsWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUser.SelectedTab = tcUser.TabPages["tpLoginInfo"];
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonDeatailsWithFilter1.FilterFocus();

            }

        
    }

        private void frmAddEditUserInfo_Activated(object sender, EventArgs e)
        {
            ctrlPersonDeatailsWithFilter1.FilterFocus();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received
            _SelectedPersonID = PersonID;
            ctrlPersonDeatailsWithFilter1.LoadPersonInfo(PersonID);


        }
        private void ctrlPersonDeatailsWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }
    }

  
    
}
