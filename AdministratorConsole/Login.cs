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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var responseStatusCode = await RestHelper.Login(textBoxEmail.Text, textBoxPassword.Text);
            
            if (responseStatusCode == HttpStatusCode.OK)
            {
                var frm = new Dashboard(this)
                {
                    Location = this.Location,
                    StartPosition = FormStartPosition.Manual,
                };
                frm.Show();
                Hide();
                textBoxEmail.Text = "";
                textBoxPassword.Text = "";
            } else
            {
                if (responseStatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Invalid credentials");
                } else if (responseStatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Your account has been disabled");
                } else
                {
                    MessageBox.Show("An error occurred");
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
