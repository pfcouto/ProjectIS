﻿using System;
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
                List<ExternalEntity> externalEntitiesReceived = externalEntities.Item2;
                if (externalEntitiesReceived.Count <= 0)
                {
                    MessageBox.Show("No external entities registered yet");
                    Dispose();
                }

                var all = new ExternalEntity()
                {
                    Id = -1,
                    Name = ""
                };
                externalEntitiesReceived.Insert(0, all);
                comboBoxExternalEntity.DataSource = externalEntitiesReceived;
                comboBoxExternalEntity.DisplayMember = "Name";
                comboBoxExternalEntity.ValueMember = "Id";
            }
            else
            {
                MessageBox.Show("Some error occured while fecthing external entities");
                Dispose();
            }

            comboBoxType.DataSource = types;

            fetchTransactions();
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

        private async void fetchTransactions()
        {
            int id = comboBoxExternalEntity.SelectedIndex >= 0 ? Convert.ToInt32(comboBoxExternalEntity.SelectedValue.ToString()) : -1;
            var response = await RestHelper.GetTransactions(comboBoxType.GetItemText(comboBoxType.SelectedItem), id);
            if (response.Item1 == HttpStatusCode.OK)
            {
                labelCounter.Text = response.Item2.Count + " Transaction(s)";
                LoadDataGridTransactions(response.Item2);
            }
            else
            {
                MessageBox.Show("Some error occured while fetching transactions");
                labelCounter.Text = response.Item2.Count + " Transaction(s)";
                dataGridViewTransactions.Rows.Clear();
            }
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            fetchTransactions();
        }
    }
}