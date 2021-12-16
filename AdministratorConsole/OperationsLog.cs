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
    public partial class OperationsLog : Form
    {
        public OperationsLog()
        {
            InitializeComponent();
        }

        private void OperationsLog_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private async void LoadLogs()
        {
            var response = await RestHelper.GetLogs();
            
            if (response.Item1 == HttpStatusCode.OK)
            {
                listBoxLogs.DataSource = response.Item2;
            } else
            {
                MessageBox.Show("An error occurred while loading the operations log");
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }
    }
}
