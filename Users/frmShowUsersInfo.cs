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
    public partial class frmShowUsersInfo : Form
    {
        int _UserID;
      
        public frmShowUsersInfo(int UserID)
        {
            _UserID = UserID;
            InitializeComponent();
        
        }
      

        private void frmShowUsersInfo_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoadUserInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlUserInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
