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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }

        private async void Users_Load(object sender, EventArgs e)
        {
            var response = await RestHelper.GetUsers();

            if (response.Item1 == HttpStatusCode.OK)
            {
                foreach (UserExternalEntity user in response.Item2)
                {
                    dataGridViewUsers.BeginInvoke((MethodInvoker)delegate { dataGridViewUsers.Rows.Add(user.PhoneNumber, user.ExternalEntityId); });
                }
            }
            else
            {
                MessageBox.Show("An error occurred while loading users");
            }
        }
    }
}
