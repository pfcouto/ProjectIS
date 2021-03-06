using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdministratorConsole
{
    public partial class Users : Form
    {
        private List<ExternalEntity> externalEntities;

        Image pictureImage = null;
        public Users()
        {
            InitializeComponent();
        }

        private async void Users_Load(object sender, EventArgs e)
        {
            pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;

            var response = await RestHelper.GetExternalEntities();
            if (response.Item1 == HttpStatusCode.OK)
            {
                if (response.Item2.Count > 0)
                {
                    externalEntities = response.Item2;
                    comboBoxExternalEntity.DataSource = response.Item2;
                    comboBoxExternalEntity.ValueMember = "Id";
                    comboBoxExternalEntity.DisplayMember = "Name";

                }
                else
                {
                    var x = MessageBox.Show("No external entities registered");
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("An error occurred while fetching external entities");
                this.Dispose();
            }
        }

        private async void buttonCreateUser_Click(object sender, EventArgs e)
        {
            if (comboBoxExternalEntity.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select an External Entity");
                return;
            }
            if (textBoxName.Text.Length < 1)
            {
                MessageBox.Show("Please insert a Name");
                return;
            }
            if (textBoxEmail.Text.Length < 1)
            {
                MessageBox.Show("Please insert an Email");
                return;
            }
            var regexEmail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!regexEmail.IsMatch(textBoxEmail.Text))
            {
                MessageBox.Show("Email has an invalid format");
                return;
            }
            if (textBoxPhoneNumber.Text.Length < 1)
            {
                MessageBox.Show("Please insert a Phone Number");
                return;
            }
            var phoneRegex = new Regex(@"^[9][1-3,6][0-9]{7}$");
            if (!phoneRegex.IsMatch(textBoxPhoneNumber.Text))
            {
                MessageBox.Show("The Phone Number has an invalid format");
                return;
            }
            if (textBoxPassword.Text.Length < 1)
            {
                MessageBox.Show("Password cannot be empty");
                return;
            }

            if (textBoxConfirmationCode.Text.Length < 1)
            {
                MessageBox.Show("Confirmation Code cannot be empty");
                return;
            }

            byte[] byteArray = null;

            if (pictureImage != null)
            {
                using (var ms = new MemoryStream())
                {
                    pictureImage.Save(ms, ImageFormat.Jpeg);
                    byteArray = ms.ToArray();
                }
            }
            string base64Image = byteArray == null ? null : Convert.ToBase64String(byteArray);


            var response = await RestHelper.CreateUser(Convert.ToInt32(comboBoxExternalEntity.SelectedValue.ToString()), textBoxName.Text, textBoxEmail.Text, textBoxPhoneNumber.Text, textBoxPassword.Text, textBoxConfirmationCode.Text, base64Image);

            if (response == HttpStatusCode.OK)
            {
                MessageBox.Show("User created with success");
                textBoxConfirmationCode.Text = "";
                textBoxEmail.Text = "";
                textBoxPassword.Text = "";
                textBoxName.Text = "";
                textBoxPhoneNumber.Text = "";
                textBoxPicture.Text = "";
                pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
                pictureImage = null;
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void buttonChoosePicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFile = openFileDialog1.FileName;
                textBoxPicture.Text = selectedFile;
                pictureImage = Image.FromFile(selectedFile);
                pictureBoxPhoto.Image = pictureImage;
            }
        }
    }
}
