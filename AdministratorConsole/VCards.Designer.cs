
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
            this.PhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExternalEntity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRefreshBalance = new System.Windows.Forms.Button();
            this.totalVCards = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVCards)).BeginInit();
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
            this.Balance});
            this.dataGridViewVCards.Location = new System.Drawing.Point(43, 69);
            this.dataGridViewVCards.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewVCards.Name = "dataGridViewVCards";
            this.dataGridViewVCards.ReadOnly = true;
            this.dataGridViewVCards.RowHeadersWidth = 51;
            this.dataGridViewVCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewVCards.Size = new System.Drawing.Size(746, 274);
            this.dataGridViewVCards.TabIndex = 0;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "VCards";
            // 
            // buttonRefreshBalance
            // 
            this.buttonRefreshBalance.Location = new System.Drawing.Point(621, 361);
            this.buttonRefreshBalance.Name = "buttonRefreshBalance";
            this.buttonRefreshBalance.Size = new System.Drawing.Size(168, 47);
            this.buttonRefreshBalance.TabIndex = 2;
            this.buttonRefreshBalance.Text = "Refresh Balance";
            this.buttonRefreshBalance.UseVisualStyleBackColor = true;
            this.buttonRefreshBalance.Click += new System.EventHandler(this.buttonRefreshBalance_Click);
            // 
            // totalVCards
            // 
            this.totalVCards.AutoSize = true;
            this.totalVCards.Location = new System.Drawing.Point(40, 361);
            this.totalVCards.Name = "totalVCards";
            this.totalVCards.Size = new System.Drawing.Size(0, 17);
            this.totalVCards.TabIndex = 3;
            // 
            // VCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 437);
            this.Controls.Add(this.totalVCards);
            this.Controls.Add(this.buttonRefreshBalance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewVCards);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(831, 484);
            this.MinimumSize = new System.Drawing.Size(831, 484);
            this.Name = "VCards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VCards";
            this.Load += new System.EventHandler(this.Users_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVCards)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewVCards;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhoneNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExternalEntity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRefreshBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.Label totalVCards;
    }
}