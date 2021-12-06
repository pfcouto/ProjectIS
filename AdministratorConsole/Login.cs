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
                var frm = new Dashboard
                {
                    Location = this.Location,
                    StartPosition = FormStartPosition.Manual
                };
                frm.Show();
                Hide();
            } else
            {
                if (responseStatusCode == HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Invalid credentials");
                } else
                {
                    MessageBox.Show("An error occurred");
                }
            }
        }
    }
}
