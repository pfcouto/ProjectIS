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
    public partial class Profile : Form
    {
        private string name;
        private string email;

        public Profile(string name, string email)
        {
            this.name = name;
            this.email = email;
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            textBoxName.Text = name;
            textBoxEmail.Text = email;
        }

        private async void buttonChangePassword_Click(object sender, EventArgs e)
        {
            var response = await RestHelper.ChangePassword(textBoxOldPassword.Text, textBoxNewPassword.Text);

            if (response == HttpStatusCode.OK)
            {
                MessageBox.Show("Password changed with success");
            } else
            {
                MessageBox.Show("An error occurred while updating the password");
            }
        }
    }
}
