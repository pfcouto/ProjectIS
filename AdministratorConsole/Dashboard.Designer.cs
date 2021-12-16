
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
            this.buttonUsers = new System.Windows.Forms.Button();
            this.buttonTransactions = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.buttonMyProfile = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelVcardsRegistered = new System.Windows.Forms.Label();
            this.labelNumberOfTransactions = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.buttonVCards.Location = new System.Drawing.Point(243, 73);
            this.buttonVCards.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonVCards.Name = "buttonVCards";
            this.buttonVCards.Size = new System.Drawing.Size(155, 80);
            this.buttonVCards.TabIndex = 0;
            this.buttonVCards.Text = "VCards";
            this.buttonVCards.UseVisualStyleBackColor = true;
            this.buttonVCards.Click += new System.EventHandler(this.buttonVCards_Click);
            // 
            // buttonExternalEntities
            // 
            this.buttonExternalEntities.Location = new System.Drawing.Point(49, 204);
            this.buttonExternalEntities.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonExternalEntities.Name = "buttonExternalEntities";
            this.buttonExternalEntities.Size = new System.Drawing.Size(155, 80);
            this.buttonExternalEntities.TabIndex = 1;
            this.buttonExternalEntities.Text = "External Entities";
            this.buttonExternalEntities.UseVisualStyleBackColor = true;
            this.buttonExternalEntities.Click += new System.EventHandler(this.buttonExternalEntities_Click);
            // 
            // buttonAdmins
            // 
            this.buttonAdmins.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAdmins.Location = new System.Drawing.Point(49, 73);
            this.buttonAdmins.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAdmins.Name = "buttonAdmins";
            this.buttonAdmins.Size = new System.Drawing.Size(155, 80);
            this.buttonAdmins.TabIndex = 2;
            this.buttonAdmins.Text = "Admins";
            this.buttonAdmins.UseVisualStyleBackColor = true;
            this.buttonAdmins.Click += new System.EventHandler(this.buttonAdmins_Click);
            // 
            // buttonOperationsLog
            // 
            this.buttonOperationsLog.Location = new System.Drawing.Point(621, 156);
            this.buttonOperationsLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOperationsLog.Name = "buttonOperationsLog";
            this.buttonOperationsLog.Size = new System.Drawing.Size(219, 81);
            this.buttonOperationsLog.TabIndex = 3;
            this.buttonOperationsLog.Text = "Operations Log";
            this.buttonOperationsLog.UseVisualStyleBackColor = true;
            this.buttonOperationsLog.Click += new System.EventHandler(this.buttonOperationsLog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonUsers);
            this.groupBox1.Controls.Add(this.buttonAdmins);
            this.groupBox1.Controls.Add(this.buttonVCards);
            this.groupBox1.Controls.Add(this.buttonExternalEntities);
            this.groupBox1.Location = new System.Drawing.Point(47, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(435, 329);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Administration Tools";
            // 
            // buttonUsers
            // 
            this.buttonUsers.Location = new System.Drawing.Point(243, 204);
            this.buttonUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonUsers.Name = "buttonUsers";
            this.buttonUsers.Size = new System.Drawing.Size(155, 80);
            this.buttonUsers.TabIndex = 4;
            this.buttonUsers.Text = "Users";
            this.buttonUsers.UseVisualStyleBackColor = true;
            this.buttonUsers.Click += new System.EventHandler(this.buttonUsers_Click);
            // 
            // buttonTransactions
            // 
            this.buttonTransactions.Location = new System.Drawing.Point(621, 42);
            this.buttonTransactions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonTransactions.Name = "buttonTransactions";
            this.buttonTransactions.Size = new System.Drawing.Size(219, 81);
            this.buttonTransactions.TabIndex = 3;
            this.buttonTransactions.Text = "Transactions";
            this.buttonTransactions.UseVisualStyleBackColor = true;
            this.buttonTransactions.Click += new System.EventHandler(this.buttonTransactions_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonLogout);
            this.groupBox2.Controls.Add(this.buttonMyProfile);
            this.groupBox2.Location = new System.Drawing.Point(525, 36);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(435, 329);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Profile";
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(29, 183);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(372, 46);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // buttonMyProfile
            // 
            this.buttonMyProfile.Location = new System.Drawing.Point(29, 106);
            this.buttonMyProfile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonMyProfile.Name = "buttonMyProfile";
            this.buttonMyProfile.Size = new System.Drawing.Size(372, 47);
            this.buttonMyProfile.TabIndex = 0;
            this.buttonMyProfile.Text = "My profile";
            this.buttonMyProfile.UseVisualStyleBackColor = true;
            this.buttonMyProfile.Click += new System.EventHandler(this.buttonMyProfile_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelVcardsRegistered);
            this.groupBox3.Controls.Add(this.labelNumberOfTransactions);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.buttonTransactions);
            this.groupBox3.Controls.Add(this.buttonOperationsLog);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.labelLastOperation);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(47, 399);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(913, 290);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // labelVcardsRegistered
            // 
            this.labelVcardsRegistered.AutoSize = true;
            this.labelVcardsRegistered.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVcardsRegistered.ForeColor = System.Drawing.Color.Green;
            this.labelVcardsRegistered.Location = new System.Drawing.Point(284, 118);
            this.labelVcardsRegistered.Name = "labelVcardsRegistered";
            this.labelVcardsRegistered.Size = new System.Drawing.Size(0, 29);
            this.labelVcardsRegistered.TabIndex = 5;
            // 
            // labelNumberOfTransactions
            // 
            this.labelNumberOfTransactions.AutoSize = true;
            this.labelNumberOfTransactions.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumberOfTransactions.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelNumberOfTransactions.Location = new System.Drawing.Point(335, 67);
            this.labelNumberOfTransactions.Name = "labelNumberOfTransactions";
            this.labelNumberOfTransactions.Size = new System.Drawing.Size(0, 29);
            this.labelNumberOfTransactions.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(43, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(242, 28);
            this.label5.TabIndex = 3;
            this.label5.Text = "VCards Registered:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number of transactions:";
            // 
            // labelLastOperation
            // 
            this.labelLastOperation.AutoSize = true;
            this.labelLastOperation.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLastOperation.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelLastOperation.Location = new System.Drawing.Point(45, 226);
            this.labelLastOperation.Name = "labelLastOperation";
            this.labelLastOperation.Size = new System.Drawing.Size(310, 21);
            this.labelLastOperation.TabIndex = 1;
            this.labelLastOperation.Text = "Waiting for an operation to happen...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(44, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Operation";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 719);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1023, 766);
            this.MinimumSize = new System.Drawing.Size(1023, 766);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
            this.Load += new System.EventHandler(this.Dashboard_Load);
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelLastOperation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTransactions;
        private System.Windows.Forms.Button buttonUsers;
        private System.Windows.Forms.Label labelVcardsRegistered;
        private System.Windows.Forms.Label labelNumberOfTransactions;
    }
}