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
                    dataGridViewVCards.BeginInvoke((MethodInvoker)delegate { dataGridViewVCards.Rows.Add(user.PhoneNumber, user.ExternalEntityId, null); });
                }
            }
            else
            {
                MessageBox.Show("An error occurred while loading users");
            }
        }

        private async void buttonRefreshBalance_Click(object sender, EventArgs e)
        {
            var selectedRowsCount = dataGridViewVCards.SelectedRows.Count;
            if (selectedRowsCount < 1)
            {
                foreach (DataGridViewRow selectedRow in dataGridViewVCards.Rows)
                {
                    selectedRow.Cells[0].Value.ToString();
                    var response = await RestHelper.GetBalanceOfVCard(selectedRow.Cells[0].Value.ToString());

                    if (response.Item1 == HttpStatusCode.OK)
                    {
                        selectedRow.Cells[2].Value = response.Item2;
                        return;
                    }

                }
                return;
            }

            foreach (DataGridViewRow selectedRow in dataGridViewVCards.SelectedRows)
            {
                var response = await RestHelper.GetBalanceOfVCard(selectedRow.Cells[0].Value.ToString());

                if (response.Item1 == HttpStatusCode.OK)
                {
                    selectedRow.Cells[2].Value = response.Item2;
                    return;
                }

            }
        }
    }
}
