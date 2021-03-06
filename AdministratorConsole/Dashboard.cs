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
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace AdministratorConsole
{
    public partial class Dashboard : Form
    {
        public Form parentForm;
        public MqttClient mqttClient;
        
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

        private async void buttonMyProfile_Click(object sender, EventArgs e)
        {
            var response = await RestHelper.GetAdminInfo();
            
            if (response.Item1 == HttpStatusCode.OK)
            {
                var frm = new Profile(response.Item2, response.Item3);
                frm.Show();
            } else
            {
                MessageBox.Show("An error occurred");
            }
        }

        private void buttonAdmins_Click(object sender, EventArgs e)
        {
            var frm = new Admins();
            frm.Show();
        }

        private void buttonExternalEntities_Click(object sender, EventArgs e)
        {
            var frm = new ExternalEntities();
            frm.Show();
        }

        private void buttonVCards_Click(object sender, EventArgs e)
        {
            var frm = new VCards();
            frm.Show();
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            var frm = new Users();
            frm.Show();
        }

        private void buttonTransactions_Click(object sender, EventArgs e)
        {
            var frm = new Transactions();
            frm.Show();
        }

        private void buttonOperationsLog_Click(object sender, EventArgs e)
        {
            var frm = new OperationsLog();
            frm.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            FetchStatistics();
            try
            {
                mqttClient = new MqttClient("127.0.0.1");

                string[] topics = {"operations", "operations"};

                mqttClient.Connect(Guid.NewGuid().ToString());
                if (!mqttClient.IsConnected)
                {
                    MessageBox.Show("Error connecting to message broker");
                    return;
                }

                mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

                byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
                mqttClient.Subscribe(topics, qosLevels);
            }
            catch (Exception)
            {
            }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                FetchStatistics();
                string lastOperation = Encoding.UTF8.GetString(e.Message);
                labelLastOperation.Text = lastOperation;
            });
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mqttClient.IsConnected)
            {
                mqttClient.Disconnect();
            }
        }

        private async void FetchStatistics()
        {
            var responseVCards = await RestHelper.GetVCards();
            var responseTransactions = await RestHelper.GetTransactions();

            if (responseVCards.Item1 == HttpStatusCode.OK)
            {
                labelVcardsRegistered.Text = responseVCards.Item2.Count.ToString();
            }
            
            if (responseTransactions.Item1 == HttpStatusCode.OK)
            {
                labelNumberOfTransactions.Text = responseTransactions.Item2.Count.ToString();
            }
        }
    }
}
