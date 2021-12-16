
namespace AdministratorConsole
{
    partial class ExternalEntities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalEntities));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewEntities = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEndpoint = new System.Windows.Forms.TextBox();
            this.buttonAddEntity = new System.Windows.Forms.Button();
            this.buttonRemoveEntity = new System.Windows.Forms.Button();
            this.buttonConfigurations = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMaxDebit = new System.Windows.Forms.TextBox();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxDebit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Endpoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "External Entities List";
            // 
            // dataGridViewEntities
            // 
            this.dataGridViewEntities.AllowUserToAddRows = false;
            this.dataGridViewEntities.AllowUserToDeleteRows = false;
            this.dataGridViewEntities.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEntities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEntities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.NameColumn,
            this.MaxDebit,
            this.Endpoint,
            this.Status});
            this.dataGridViewEntities.Location = new System.Drawing.Point(55, 75);
            this.dataGridViewEntities.Name = "dataGridViewEntities";
            this.dataGridViewEntities.ReadOnly = true;
            this.dataGridViewEntities.RowHeadersWidth = 51;
            this.dataGridViewEntities.RowTemplate.Height = 24;
            this.dataGridViewEntities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEntities.Size = new System.Drawing.Size(695, 214);
            this.dataGridViewEntities.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(55, 342);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(451, 22);
            this.textBoxName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Endpoint";
            // 
            // textBoxEndpoint
            // 
            this.textBoxEndpoint.Location = new System.Drawing.Point(55, 403);
            this.textBoxEndpoint.Name = "textBoxEndpoint";
            this.textBoxEndpoint.Size = new System.Drawing.Size(451, 22);
            this.textBoxEndpoint.TabIndex = 5;
            // 
            // buttonAddEntity
            // 
            this.buttonAddEntity.Location = new System.Drawing.Point(549, 386);
            this.buttonAddEntity.Name = "buttonAddEntity";
            this.buttonAddEntity.Size = new System.Drawing.Size(201, 39);
            this.buttonAddEntity.TabIndex = 6;
            this.buttonAddEntity.Text = "Add External Entity";
            this.buttonAddEntity.UseVisualStyleBackColor = true;
            this.buttonAddEntity.Click += new System.EventHandler(this.buttonAddEntity_Click);
            // 
            // buttonRemoveEntity
            // 
            this.buttonRemoveEntity.Location = new System.Drawing.Point(561, 38);
            this.buttonRemoveEntity.Name = "buttonRemoveEntity";
            this.buttonRemoveEntity.Size = new System.Drawing.Size(189, 29);
            this.buttonRemoveEntity.TabIndex = 7;
            this.buttonRemoveEntity.Text = "Remove External Entity";
            this.buttonRemoveEntity.UseVisualStyleBackColor = true;
            this.buttonRemoveEntity.Click += new System.EventHandler(this.buttonRemoveEntity_Click);
            // 
            // buttonConfigurations
            // 
            this.buttonConfigurations.Location = new System.Drawing.Point(445, 38);
            this.buttonConfigurations.Name = "buttonConfigurations";
            this.buttonConfigurations.Size = new System.Drawing.Size(110, 29);
            this.buttonConfigurations.TabIndex = 8;
            this.buttonConfigurations.Text = "Configurations";
            this.buttonConfigurations.UseVisualStyleBackColor = true;
            this.buttonConfigurations.Click += new System.EventHandler(this.buttonConfigurations_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(546, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Max Debit";
            // 
            // textBoxMaxDebit
            // 
            this.textBoxMaxDebit.Location = new System.Drawing.Point(549, 342);
            this.textBoxMaxDebit.Name = "textBoxMaxDebit";
            this.textBoxMaxDebit.Size = new System.Drawing.Size(201, 22);
            this.textBoxMaxDebit.TabIndex = 10;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 6;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // MaxDebit
            // 
            this.MaxDebit.HeaderText = "Max Debit";
            this.MaxDebit.MinimumWidth = 6;
            this.MaxDebit.Name = "MaxDebit";
            this.MaxDebit.ReadOnly = true;
            // 
            // Endpoint
            // 
            this.Endpoint.HeaderText = "Endpoint";
            this.Endpoint.MinimumWidth = 6;
            this.Endpoint.Name = "Endpoint";
            this.Endpoint.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ExternalEntities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxMaxDebit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonConfigurations);
            this.Controls.Add(this.buttonRemoveEntity);
            this.Controls.Add(this.buttonAddEntity);
            this.Controls.Add(this.textBoxEndpoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewEntities);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(818, 497);
            this.MinimumSize = new System.Drawing.Size(818, 497);
            this.Name = "ExternalEntities";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExternalEntities";
            this.Load += new System.EventHandler(this.ExternalEntities_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEntities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewEntities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEndpoint;
        private System.Windows.Forms.Button buttonAddEntity;
        private System.Windows.Forms.Button buttonRemoveEntity;
        private System.Windows.Forms.Button buttonConfigurations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMaxDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxDebit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Endpoint;
        private System.Windows.Forms.DataGridViewImageColumn Status;
    }
}