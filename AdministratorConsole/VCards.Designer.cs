
namespace AdministratorConsole
{
    partial class VCards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VCards));
            this.dataGridViewVCards = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRefreshBalance = new System.Windows.Forms.Button();
            this.totalVCards = new System.Windows.Forms.Label();
            this.buttonEarningPercentage = new System.Windows.Forms.Button();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExternalEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EarningPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVCards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewVCards
            // 
            this.dataGridViewVCards.AllowUserToAddRows = false;
            this.dataGridViewVCards.AllowUserToDeleteRows = false;
            this.dataGridViewVCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewVCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PhoneNumber,
            this.ExternalEntity,
            this.Balance,
            this.EarningPercentage});
            this.dataGridViewVCards.Location = new System.Drawing.Point(32, 56);
            this.dataGridViewVCards.Name = "dataGridViewVCards";
            this.dataGridViewVCards.ReadOnly = true;
            this.dataGridViewVCards.RowHeadersWidth = 51;
            this.dataGridViewVCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVCards.Size = new System.Drawing.Size(560, 223);
            this.dataGridViewVCards.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "VCards";
            // 
            // buttonRefreshBalance
            // 
            this.buttonRefreshBalance.Location = new System.Drawing.Point(467, 302);
            this.buttonRefreshBalance.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRefreshBalance.Name = "buttonRefreshBalance";
            this.buttonRefreshBalance.Size = new System.Drawing.Size(126, 38);
            this.buttonRefreshBalance.TabIndex = 2;
            this.buttonRefreshBalance.Text = "Fetch VCard Info";
            this.buttonRefreshBalance.UseVisualStyleBackColor = true;
            this.buttonRefreshBalance.Click += new System.EventHandler(this.buttonRefreshVCardDetails_Click);
            // 
            // totalVCards
            // 
            this.totalVCards.AutoSize = true;
            this.totalVCards.Location = new System.Drawing.Point(30, 293);
            this.totalVCards.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.totalVCards.Name = "totalVCards";
            this.totalVCards.Size = new System.Drawing.Size(0, 13);
            this.totalVCards.TabIndex = 3;
            // 
            // buttonEarningPercentage
            // 
            this.buttonEarningPercentage.Location = new System.Drawing.Point(288, 302);
            this.buttonEarningPercentage.Name = "buttonEarningPercentage";
            this.buttonEarningPercentage.Size = new System.Drawing.Size(174, 38);
            this.buttonEarningPercentage.TabIndex = 4;
            this.buttonEarningPercentage.Text = "Update Earning Percentage";
            this.buttonEarningPercentage.UseVisualStyleBackColor = true;
            this.buttonEarningPercentage.Click += new System.EventHandler(this.buttonEarningPercentage_Click);
            // 
            // numericUpDown
            // 
            this.numericUpDown.DecimalPlaces = 2;
            this.numericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown.Location = new System.Drawing.Point(195, 313);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(87, 20);
            this.numericUpDown.TabIndex = 6;
            // 
            // PhoneNumber
            // 
            this.PhoneNumber.HeaderText = "Phone Number";
            this.PhoneNumber.MinimumWidth = 6;
            this.PhoneNumber.Name = "PhoneNumber";
            this.PhoneNumber.ReadOnly = true;
            // 
            // ExternalEntity
            // 
            this.ExternalEntity.HeaderText = "External Entity Code";
            this.ExternalEntity.MinimumWidth = 6;
            this.ExternalEntity.Name = "ExternalEntity";
            this.ExternalEntity.ReadOnly = true;
            // 
            // Balance
            // 
            this.Balance.HeaderText = "Balance";
            this.Balance.MinimumWidth = 6;
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            // 
            // EarningPercentage
            // 
            this.EarningPercentage.HeaderText = "Earning Percentage";
            this.EarningPercentage.Name = "EarningPercentage";
            this.EarningPercentage.ReadOnly = true;
            // 
            // VCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 362);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.buttonEarningPercentage);
            this.Controls.Add(this.totalVCards);
            this.Controls.Add(this.buttonRefreshBalance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewVCards);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(627, 401);
            this.MinimumSize = new System.Drawing.Size(627, 401);
            this.Name = "VCards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VCards";
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVCards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewVCards;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRefreshBalance;
        private System.Windows.Forms.Label totalVCards;
        private System.Windows.Forms.Button buttonEarningPercentage;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExternalEntity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn EarningPercentage;
    }
}