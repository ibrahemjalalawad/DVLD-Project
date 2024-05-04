using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class frmShowPeopleInfo : Form
    {
        public frmShowPeopleInfo(int PersonID)
        {
            InitializeComponent();
            ctrlPersonDetails1.LoadPersonInfo(PersonID);
        }
        public frmShowPeopleInfo(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonDetails1.LoadPersonInfo(NationalNo);
        }
        private void frmShowPeopleInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
