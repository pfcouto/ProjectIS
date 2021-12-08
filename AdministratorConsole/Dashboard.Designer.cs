
namespace AdministratorConsole
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.buttonVCards = new System.Windows.Forms.Button();
            this.buttonExternalEntities = new System.Windows.Forms.Button();
            this.buttonAdmins = new System.Windows.Forms.Button();
            this.buttonOperationsLog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonTransactions = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonMyProfile = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonUsers = new System.Windows.Forms.Button();
            this.labelNumberOfVcards = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelLastOperation = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonVCards
            // 
            this.buttonVCards.Location = new System.Drawing.Point(182, 59);
            this.buttonVCards.Margin = new System.Windows.Forms.Padding(2);
            this.buttonVCards.Name = "buttonVCards";
            this.buttonVCards.Size = new System.Drawing.Size(116, 65);
            this.buttonVCards.TabIndex = 0;
            this.buttonVCards.Text = "VCards";
            this.buttonVCards.UseVisualStyleBackColor = true;
            this.buttonVCards.Click += new System.EventHandler(this.buttonVCards_Click);
            // 
            // buttonExternalEntities
            // 
            this.buttonExternalEntities.Location = new System.Drawing.Point(37, 166);
            this.buttonExternalEntities.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExternalEntities.Name = "buttonExternalEntities";
            this.buttonExternalEntities.Size = new System.Drawing.Size(116, 65);
            this.buttonExternalEntities.TabIndex = 1;
            this.buttonExternalEntities.Text = "External Entities";
            this.buttonExternalEntities.UseVisualStyleBackColor = true;
            this.buttonExternalEntities.Click += new System.EventHandler(this.buttonExternalEntities_Click);
            // 
            // buttonAdmins
            // 
            this.buttonAdmins.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAdmins.Location = new System.Drawing.Point(37, 59);
            this.buttonAdmins.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdmins.Name = "buttonAdmins";
            this.buttonAdmins.Size = new System.Drawing.Size(116, 65);
            this.buttonAdmins.TabIndex = 2;
            this.buttonAdmins.Text = "Admins";
            this.buttonAdmins.UseVisualStyleBackColor = true;
            this.buttonAdmins.Click += new System.EventHandler(this.buttonAdmins_Click);
            // 
            // buttonOperationsLog
            // 
            this.buttonOperationsLog.Location = new System.Drawing.Point(466, 127);
            this.buttonOperationsLog.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOperationsLog.Name = "buttonOperationsLog";
            this.buttonOperationsLog.Size = new System.Drawing.Size(164, 66);
            this.buttonOperationsLog.TabIndex = 3;
            this.buttonOperationsLog.Text = "Operations Log";
            this.buttonOperationsLog.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonUsers);
            this.groupBox1.Controls.Add(this.buttonAdmins);
            this.groupBox1.Controls.Add(this.buttonVCards);
            this.groupBox1.Controls.Add(this.buttonExternalEntities);
            this.groupBox1.Location = new System.Drawing.Point(35, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(326, 267);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Administration Tools";
            // 
            // buttonTransactions
            // 
            this.buttonTransactions.Location = new System.Drawing.Point(466, 34);
            this.buttonTransactions.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTransactions.Name = "buttonTransactions";
            this.buttonTransactions.Size = new System.Drawing.Size(164, 66);
            this.buttonTransactions.TabIndex = 3;
            this.buttonTransactions.Text = "Transactions";
            this.buttonTransactions.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLogout);
            this.groupBox2.Controls.Add(this.buttonMyProfile);
            this.groupBox2.Location = new System.Drawing.Point(394, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(326, 267);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Profile";
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(22, 149);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(279, 37);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonMyProfile
            // 
            this.buttonMyProfile.Location = new System.Drawing.Point(22, 86);
            this.buttonMyProfile.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMyProfile.Name = "buttonMyProfile";
            this.buttonMyProfile.Size = new System.Drawing.Size(279, 38);
            this.buttonMyProfile.TabIndex = 0;
            this.buttonMyProfile.Text = "My profile";
            this.buttonMyProfile.UseVisualStyleBackColor = true;
            this.buttonMyProfile.Click += new System.EventHandler(this.buttonMyProfile_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelNumberOfVcards);
            this.groupBox3.Controls.Add(this.buttonTransactions);
            this.groupBox3.Controls.Add(this.buttonOperationsLog);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.labelLastOperation);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(35, 324);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(685, 236);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // buttonUsers
            // 
            this.buttonUsers.Location = new System.Drawing.Point(182, 166);
            this.buttonUsers.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(116, 65);
            this.buttonUsers.TabIndex = 4;
            this.buttonUsers.Text = "Users";
            this.buttonUsers.UseVisualStyleBackColor = true;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // labelNumberOfVcards
            // 
            this.labelNumberOfVcards.AutoSize = true;
            this.labelNumberOfVcards.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumberOfVcards.Location = new System.Drawing.Point(32, 96);
            this.labelNumberOfVcards.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNumberOfVcards.Name = "labelNumberOfVcards";
            this.labelNumberOfVcards.Size = new System.Drawing.Size(198, 24);
            this.labelNumberOfVcards.TabIndex = 3;
            this.labelNumberOfVcards.Text = "VCards Registered: 10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number of transactions: 200";
            // 
            // labelLastOperation
            // 
            this.labelLastOperation.AutoSize = true;
            this.labelLastOperation.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastOperation.Location = new System.Drawing.Point(33, 175);
            this.labelLastOperation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLastOperation.Name = "labelLastOperation";
            this.labelLastOperation.Size = new System.Drawing.Size(352, 18);
            this.labelLastOperation.TabIndex = 1;
            this.labelLastOperation.Text = "Transaction of 20€ between 910000002 and 910000001";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 136);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Operation";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 592);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(772, 631);
            this.MinimumSize = new System.Drawing.Size(772, 631);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonVCards;
        private System.Windows.Forms.Button buttonExternalEntities;
        private System.Windows.Forms.Button buttonAdmins;
        private System.Windows.Forms.Button buttonOperationsLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Button buttonMyProfile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labelNumberOfVcards;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLastOperation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTransactions;
        private System.Windows.Forms.Button buttonUsers;
    }
}