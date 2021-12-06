using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministratorConsole
{
    public partial class Dashboard : Form
    {
        public Form parentForm;
        
        public Dashboard(Form parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            RestHelper.Logout();
            parentForm.Show();
            Close();
        }
    }
}
