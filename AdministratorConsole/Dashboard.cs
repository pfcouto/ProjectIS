using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministratorConsole
{
    public partial class Dashboard : Form
    {
        public Form parentForm;
        
        public Dashboard(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            RestHelper.Logout();
            parentForm.Show();
            Close();
        }

        private async void buttonMyProfile_Click(object sender, EventArgs e)
        {
            var response = await RestHelper.GetAdminInfo();
            
            if (response.Item1 == HttpStatusCode.OK)
            {
                var frm = new Profile(response.Item2, response.Item3);
                frm.Show();
            } else
            {
                MessageBox.Show("An error occurred");
            }
        }

        private void buttonAdmins_Click(object sender, EventArgs e)
        {
            var frm = new Admins();
            frm.Show();
        }

        private void buttonExternalEntities_Click(object sender, EventArgs e)
        {
            var frm = new ExternalEntities();
            frm.Show();
        }

        private void buttonVCards_Click(object sender, EventArgs e)
        {
            var frm = new VCards();
            frm.Show();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            var frm = new Users();
            frm.Show();
        }
    }
}
