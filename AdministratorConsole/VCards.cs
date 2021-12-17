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
using Newtonsoft.Json;

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

        private async void buttonRefreshVCardDetails_Click(object sender, EventArgs e)
        {
            var selectedRowsCount = dataGridViewVCards.SelectedRows.Count;
            if (selectedRowsCount == 0)
            {
                fetchBalanceEarningPercentage(true);
            }
            else
            {
                fetchBalanceEarningPercentage(false);
            }
        }

        private async void fetchBalanceEarningPercentage(Boolean all)
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
                    var response = await RestHelper.GetBalanceEarningPercentageOfVCard(selectedRow.Cells[0].Value.ToString());

                    if (response.Item1 == HttpStatusCode.OK)
                    {
                        counter++;
                        selectedRow.Cells["Balance"].Value = response.Item2.Balance;
                        selectedRow.Cells["EarningPercentage"].Value = response.Item2.EarningPercentage;
                    }

                }
            }
            else
            {
                selectedRowsCount = dataGridViewVCards.Rows.Count;
                foreach (DataGridViewRow selectedRow in dataGridViewVCards.Rows)
                {
                    selectedRow.Cells[0].Value.ToString();
                    var response = await RestHelper.GetBalanceEarningPercentageOfVCard(selectedRow.Cells[0].Value.ToString());

                    if (response.Item1 == HttpStatusCode.OK)
                    {
                        counter++;
                        selectedRow.Cells["Balance"].Value = response.Item2.Balance;
                        selectedRow.Cells["EarningPercentage"].Value = response.Item2.EarningPercentage;
                    }

                }
            }

            MessageBox.Show("Fetched " + counter + "/" + selectedRowsCount + " with success");
        }

        private async void buttonEarningPercentage_Click(object sender, EventArgs e)
        {
            decimal value = numericUpDown.Value;
            int selectedRowsCount = dataGridViewVCards.SelectedRows.Count;
            int counter = 0;
            foreach (DataGridViewRow selectedRow in dataGridViewVCards.SelectedRows)
            {
                var response = await RestHelper.PatchEarningPercentage(selectedRow.Cells[0].Value.ToString(),value);

                if (response == HttpStatusCode.OK)
                {
                    counter++;
                    selectedRow.Cells["EarningPercentage"].Value = value;
                }

            }

            MessageBox.Show("Updated " + counter + "/" + selectedRowsCount + " with success");
        }
    }
}
