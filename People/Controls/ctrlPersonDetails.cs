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
using System.IO;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Resources;
using DVLD_Project.Properties;

namespace DVLD_Project
{
    public partial class ctrlPersonDetails : UserControl
    {
        private clsPerson _Person;
        private int _PersonID = -1;
        public int PersonID {
            get {
                return _PersonID;
                }
        }
        public clsPerson SelectedPersonInfo {  get { return _Person;} }
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }

     
        public void _ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblName.Text = "[???]";
            lblEmail.Text = "[???]";
            lblAddress.Text = "[???]";
            lblCountry.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblPhone.Text = "[???]";
            lblGender.Text = "[???]";
            pbPerson.Image = Resources.Male_512;

        }
        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                pbPerson.Image = Resources.Male_512;
            else
                pbPerson.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPerson.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;
            llblEditPersonInfo.Enabled = true;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo; ;
            lblName.Text = _Person.FirstName +" "+ _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountryPersonID).CountryName;

            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();

            lblPhone.Text = _Person.Phone;
            lblGender.Text = _Person.Gender == 0 ? "Male" : "Female";
            pbPerson.Image = Resources.Male_512;
            _LoadPersonImage();

        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person with National No. = " + NationalNo.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person with Person ID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();

        }
       

        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {
         
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            frm.ShowDialog();

            //refresh
            LoadPersonInfo(_PersonID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lblPersonID_Click(object sender, EventArgs e)
        {

        }
    }
    }

