using BussinessLayesDVLD;
using DVLD_Project.Global_Classes;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BussinessLayesDVLD.clsPerson;

namespace DVLD_Project
{
    public partial class frmAddEditPersonInfo : Form
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public enum enMode
        { UpdatePerson = 0, AddPerson = 1 }
        public enum enGender { Male = 0, Female = 1 };
       
        private enMode _Mode;
        private int _PersonID=-1;
        clsPerson _Person;
        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _Mode = enMode.AddPerson;

        }
        public frmAddEditPersonInfo(int PersonID)
        {

            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.UpdatePerson;


        }

        private void _FillCountriesInComboBox()
        {
            DataTable dt = clsCountry.GetAllCountry();
            foreach (DataRow row in dt.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }


        private void _RefreshDefaultValues()
        {
           
         
            tbFirstName.Text = "";
            tbSecondName.Text = "";
            tbThirdName.Text = "";
            tbLastName.Text = "";
            tbNationalNo.Text = "";
            if (rbMale.Checked)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;
            rbMale.Checked=true;
            rtbAddress.Text = "";
            tbPhone.Text = "";
            tbEmail.Text = "";
           
            llblRemoveImage.Visible = (pbPerson.ImageLocation != null);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);
          

        }

        private void _Load()
        {

            _FillCountriesInComboBox();

            if (_Mode == enMode.AddPerson)
            {
                label1.Text = "Add New Person";
                _Person = new clsPerson();
                return;

            }

            else
            {
               
                label1.Text = "Update Person";

            }
            _RefreshDefaultValues();
        }

        private void rbMale_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbMale.Checked == true)
            {
                pbPerson.Image = Properties.Resources.Male_512;
            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(tbNationalNo, null);
            }
            if (tbNationalNo.Text != _Person.NationalNo && clsPerson.isPersonExist(tbNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNo, "National Number Already Used");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNationalNo, "");

            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (tbEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidating.ValidateEmail(tbEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(tbEmail, null);
            };
        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbFirstName_Validating_1(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void rbFemale_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbFemale.Checked == true)
            {
                pbPerson.Image = Properties.Resources.Female_512;
            }
        }
        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblPersonID.Text = _PersonID.ToString();
            tbFirstName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;
            tbNationalNo.Text = _Person.NationalNo;
            dateTimePicker1.Value = _Person.DateOfBirth;
            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            if (_Person.ImagePath != "")
            {
                pbPerson.ImageLocation = _Person.ImagePath;

            }
          

            llblRemoveImage.Visible = (_Person.ImagePath != "");

            rtbAddress.Text = _Person.Address;
            tbPhone.Text = _Person.Phone;
            tbEmail.Text = _Person.Email;
            cbCountries.SelectedIndex =cbCountries.FindString(_Person.CountryInfo.CountryName);

          
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            _Load();
            cbCountries.SelectedIndex = cbCountries.FindString("Jordan");
            if (_Mode == enMode.UpdatePerson)
            {
                _LoadData();
            }
        }

        private void llblRemoveImage_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPerson.ImageLocation = null;

          

            if (rbMale.Checked)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;

            llblRemoveImage.Visible = false;
           
        }

        private void llblSetImage_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                pbPerson.ImageLocation = selectedFilePath;
                llblRemoveImage.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPerson.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    //first we delete the old image from the folder in case there is any.

                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("error in line 289");
                        // We could not delete the file.
                        //log it later   
                    }
                }
                if (pbPerson.ImageLocation != null)
                {
                    string FilePath = pbPerson.ImageLocation.ToString();
                    if(clsUtil.SaveImageFromProjectToFile(ref FilePath))
                    {
                        pbPerson.ImageLocation = FilePath;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                
                }
               
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (!_HandlePersonImage())
                return;

            int NationalityCountryID = clsCountry.Find(cbCountries.Text).ID;

            _Person.FirstName = tbFirstName.Text.Trim();
            _Person.SecondName = tbSecondName.Text.Trim();
            _Person.ThirdName = tbThirdName.Text.Trim();
            _Person.LastName = tbLastName.Text.Trim();
            _Person.NationalNo = tbNationalNo.Text.Trim();
            _Person.Email = tbEmail.Text.Trim();
            _Person.Phone = tbPhone.Text.Trim();
            _Person.Address = rtbAddress.Text.Trim();
            _Person.DateOfBirth = dateTimePicker1.Value;

            if (rbMale.Checked)
                _Person.Gender = (byte)enGender.Male;
            else
                _Person.Gender = (byte)enGender.Female;

            _Person.NationalityCountryPersonID = NationalityCountryID;

            if (pbPerson.ImageLocation != null)
                _Person.ImagePath = pbPerson.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.UpdatePerson;
                label1.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}

