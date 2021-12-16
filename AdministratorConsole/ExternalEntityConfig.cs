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
    public partial class ExternalEntityConfig : Form
    {
        public int id;
        public DataGridViewRow row;

        public ExternalEntityConfig(DataGridViewRow row)
        {
            this.id = (int)row.Cells[0].Value;
            this.row = row;
            InitializeComponent();
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            decimal max_debit;

            if (!decimal.TryParse(textBoxMaxDebit.Text, out max_debit))
            {
                MessageBox.Show("Invalid max debit value");
                return;
            }

            var response = await RestHelper.ChangeEntityMaxDebit(id, max_debit.ToString().Replace(',', '.'));

            if (response == HttpStatusCode.OK)
            {
                MessageBox.Show("Max debit updated successfully");
                row.Cells[2].Value = max_debit.ToString();
                Close();
            }
            else
            {
                MessageBox.Show("There was an error updating the max debit");
            }
        }
    }
}
