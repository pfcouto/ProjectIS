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
    public partial class ExternalEntities : Form
    {
        public ExternalEntities()
        {
            InitializeComponent();
        }

        private async void ExternalEntities_Load(object sender, EventArgs e)
        {
            var response = await RestHelper.GetExternalEntities();
            
            if (response.Item1 == HttpStatusCode.OK)
            {
                foreach (ExternalEntity externalEntity in response.Item2)
                {
                    string imagePath = Application.StartupPath + @"\..\.." + (externalEntity.Status == '1' ?  @"\assets\green.png" : @"\assets\red.png");
                    string imageBase64 = ImageHandler.ImageToBase64String(imagePath);
                    Bitmap btmImage = ImageHandler.Base64StringToImage(imageBase64);
                    dataGridViewEntities.BeginInvoke((MethodInvoker)delegate { dataGridViewEntities.Rows.Add(externalEntity.Id, externalEntity.Name, externalEntity.Endpoint, btmImage); });  
                }
            } else
            {
                MessageBox.Show("An error occurred while loading the external entities");
            }
        }

        private async void buttonRemoveEntity_Click(object sender, EventArgs e)
        {
            int rowsDeletedWithSuccess = 0;
            int totalSelectedRows = dataGridViewEntities.SelectedRows.Count;

            foreach (DataGridViewRow item in dataGridViewEntities.SelectedRows)
            {
                var response = await RestHelper.DeleteExternalEntity(int.Parse(item.Cells[0].Value.ToString()));

                if (response == HttpStatusCode.OK)
                {
                    rowsDeletedWithSuccess++;
                    dataGridViewEntities.Rows.Remove(item);
                }
            }

            MessageBox.Show("Deleted " + rowsDeletedWithSuccess + "/" + totalSelectedRows + " external entities with success");
        }

        private async void buttonAddEntity_Click(object sender, EventArgs e)
        {
            var response = await RestHelper.CreateExternalEntity(textBoxName.Text, textBoxEndpoint.Text);

            if (response.Item1 == HttpStatusCode.OK)
            {
                string imagePath = Application.StartupPath + @"\..\.." + (response.Item2.Status == '1' ?  @"\assets\green.png" : @"\assets\red.png");
                string imageBase64 = ImageHandler.ImageToBase64String(imagePath);
                Bitmap btmImage = ImageHandler.Base64StringToImage(imageBase64);
                dataGridViewEntities.BeginInvoke((MethodInvoker)delegate { dataGridViewEntities.Rows.Add(response.Item2.Id, response.Item2.Name, response.Item2.Endpoint, btmImage); });
                MessageBox.Show("External entity created with success");
            } else if (response.Item1 == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Invalid new external entity fields");
            } else
            {
                MessageBox.Show("An error occurred while creating a new external entity");
            }
        }
    }
}
