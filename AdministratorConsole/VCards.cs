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

                totalVCards.Text = response.Item2.Count + " VCard(s)";
            }
            else
            {
                MessageBox.Show("An error occurred while loading users");
            }
        }

        private async void buttonRefreshBalance_Click(object sender, EventArgs e)
        {
            var selectedRowsCount = dataGridViewVCards.SelectedRows.Count;
            if (selectedRowsCount == 0)
            {
                fetchBalance(true);
            }
            else
            {
                fetchBalance(false);
            }
        }

        private async void fetchBalance(Boolean all)
        {
            var selectedRowsCount = 0;
            var counter = 0;
            DataGridViewRowCollection rows = null;
            DataGridViewSelectedRowCollection rowsSelected = null;

            if (!all)
            {
                selectedRowsCount = dataGridViewVCards.SelectedRows.Count;
                foreach (DataGridViewRow selectedRow in dataGridViewVCards.SelectedRows)
                {
                    selectedRow.Cells[0].Value.ToString();
                    var response = await RestHelper.GetBalanceOfVCard(selectedRow.Cells[0].Value.ToString());

                    if (response.Item1 == HttpStatusCode.OK)
                    {
                        counter++;
                        selectedRow.Cells["Balance"].Value = response.Item2;
                    }

                }
            }
            else
            {
                selectedRowsCount = dataGridViewVCards.Rows.Count;
                foreach (DataGridViewRow selectedRow in dataGridViewVCards.Rows)
                {
                    selectedRow.Cells[0].Value.ToString();
                    var response = await RestHelper.GetBalanceOfVCard(selectedRow.Cells[0].Value.ToString());

                    if (response.Item1 == HttpStatusCode.OK)
                    {
                        counter++;
                        selectedRow.Cells["Balance"].Value = response.Item2;
                    }

                }
            }

            MessageBox.Show("Fetched " + counter + "/" + selectedRowsCount + " with success");
        }

    }
}
