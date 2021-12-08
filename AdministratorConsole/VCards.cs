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
    public partial class VCards : Form
    {
        public VCards()
        {
            InitializeComponent();
        }

        private async void Users_Load(object sender, EventArgs e)
        {
            var response = await RestHelper.GetVCards();

            if (response.Item1 == HttpStatusCode.OK)
            {
                foreach (VCardExternalEntity user in response.Item2)
                {
                    dataGridViewVCards.BeginInvoke((MethodInvoker)delegate { dataGridViewVCards.Rows.Add(user.PhoneNumber, user.ExternalEntityId); });
                }
            }
            else
            {
                MessageBox.Show("An error occurred while loading users");
            }
        }
    }
}
