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
    public partial class Transactions : Form
    {
        private List<string> types;

        public Transactions()
        {
            InitializeComponent();
            types = new List<string>();
            types.Add("");
            types.Add("C");
            types.Add("D");
        }

        private async void Transactions_Load(object sender, EventArgs e)
        {
            dateTimePickerOrigin.Value = DateTime.Today;
            dateTimePickerTo.Value = DateTime.Now;

            var externalEntities = await RestHelper.GetExternalEntities();
            if (externalEntities.Item1 == HttpStatusCode.OK)
            {
                if (externalEntities.Item2.Count <= 0)
                {
                    MessageBox.Show("No external entities registered yet");
                    Dispose();
                }
                comboBoxExternalEntity.DataSource = externalEntities.Item2;
                comboBoxExternalEntity.DisplayMember = "Name";
                comboBoxExternalEntity.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("Some error occured while fecthing external entities");
                Dispose();
            }

            comboBoxType.DataSource = types;

            var response = await RestHelper.GetTransactions();
            if (response.Item1 == HttpStatusCode.OK)
            {
                LoadDataGridTransactions(response.Item2);
            }
        }

        private async void comboBoxExternalEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var response = await RestHelper.GetTransactions();
            if (response.Item1 == HttpStatusCode.OK)
            {
                LoadDataGridTransactions(response.Item2);
            }
            else
            {
                MessageBox.Show("Some error occured while fetching transactions");
            }
        }

        private void LoadDataGridTransactions(List<Transaction> transactions)
        {
            dataGridViewTransactions.BeginInvoke((MethodInvoker)delegate { dataGridViewTransactions.Rows.Clear(); });
            foreach (Transaction transaction in transactions)
            {
                if (transaction.Type == 'C')
                {
                    dataGridViewTransactions.BeginInvoke((MethodInvoker)delegate { dataGridViewTransactions.Rows.Add(transaction.Id, transaction.Type, null, transaction.VCard, transaction.Date, transaction.Value); });
                }
                else
                {
                    dataGridViewTransactions.BeginInvoke((MethodInvoker)delegate { dataGridViewTransactions.Rows.Add(transaction.Id, transaction.Type, transaction.VCard, null, transaction.Date, transaction.Value); });
                }
            }
        }

        private async void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetchTransactions();

        }

        private void dateTimePickerOrigin_ValueChanged(object sender, EventArgs e)
        {
            fetchTransactions();
        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            fetchTransactions();
        }

        private async void fetchTransactions()
        {
            var type = comboBoxType.SelectedValue != null ? comboBoxType.SelectedValue.ToString() : "";
            var response = await RestHelper.GetTransactions(type);
            if (response.Item1 == HttpStatusCode.OK)
            {
                LoadDataGridTransactions(response.Item2);
            }
            else
            {
                MessageBox.Show("Some error occured while fetching transactions");
            }
        }
    }
}
