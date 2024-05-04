using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Application_Types
{
    public partial class frmShowLocalDrivingLicenseApplicationsInfo : Form
    {
        private int _ApplicationID;
        public frmShowLocalDrivingLicenseApplicationsInfo(int applicationID)
        {
            InitializeComponent();
            _ApplicationID = applicationID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void frmShowLocalDrivingLicenseApplicationsInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_ApplicationID);
        }
    }
}
