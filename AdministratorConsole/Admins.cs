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
    public partial class Admins : Form
    {
        public Admins()
        {
            InitializeComponent();
        }

        private async void buttonAddAdmin_Click(object sender, EventArgs e)
        {
            var response = await RestHelper.CreateAdmin(textBoxName.Text, textBoxEmail.Text, textBoxPassword.Text);

            if (response.Item1 == HttpStatusCode.OK)
            {
                dataGridViewAdmins.BeginInvoke((MethodInvoker)delegate { dataGridViewAdmins.Rows.Add(response.Item2.Name, response.Item2.Email, response.Item2.Enabled); });
                MessageBox.Show("Administrator created with success");
            } else if (response.Item1 == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Invalid new admin fields");
            } else
            {
                MessageBox.Show("An error occurred while creating a new administrator");
            }
        }

        private async void Admins_Load(object sender, EventArgs e)
        {
            var response = await RestHelper.GetAdmins();
            
            if (response.Item1 == HttpStatusCode.OK)
            {
                foreach (Admin admin in response.Item2)
                {
                    dataGridViewAdmins.BeginInvoke((MethodInvoker)delegate { dataGridViewAdmins.Rows.Add(admin.Name, admin.Email, admin.Enabled); });  
                }
            } else
            {
                MessageBox.Show("An error occurred while loading admins");
            }
        }

        private async void buttonEnableDisable_Click(object sender, EventArgs e)
        {
            int rowsUpdatedWithSuccess = 0;
            int totalSelectedRows = dataGridViewAdmins.SelectedRows.Count;

            foreach (DataGridViewRow item in dataGridViewAdmins.SelectedRows)
            {
                string newEnabled = item.Cells[2].Value.ToString() == "1" ? "0" : "1";
                var response = await RestHelper.ChangeAdminEnabled(item.Cells[1].Value.ToString(), newEnabled);

                if (response == HttpStatusCode.OK)
                {
                    rowsUpdatedWithSuccess++;
                    item.Cells[2].Value = newEnabled;
                }
            }

            MessageBox.Show("Updated " + rowsUpdatedWithSuccess + "/" + totalSelectedRows + " admins with success");
        }
    }
}
