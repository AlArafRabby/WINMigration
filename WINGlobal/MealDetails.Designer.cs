
namespace WINGlobal
{
    partial class MealDetails
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MealDetails));
            this.dgvDtls = new System.Windows.Forms.DataGridView();
            this.globalData = new WINGlobal.GlobalData();
            this.sprAGCafeteriaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sprAGCafeteriaTableAdapter = new WINGlobal.GlobalDataTableAdapters.SprAGCafeteriaTableAdapter();
            this.tokenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Issue = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDtls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.globalData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sprAGCafeteriaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDtls
            // 
            this.dgvDtls.AutoGenerateColumns = false;
            this.dgvDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDtls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tokenDataGridViewTextBoxColumn,
            this.cardNoDataGridViewTextBoxColumn,
            this.Issue});
            this.dgvDtls.DataSource = this.sprAGCafeteriaBindingSource;
            this.dgvDtls.Location = new System.Drawing.Point(0, -1);
            this.dgvDtls.Name = "dgvDtls";
            this.dgvDtls.Size = new System.Drawing.Size(754, 844);
            this.dgvDtls.TabIndex = 0;
            this.dgvDtls.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDtls_CellContentClick);
            // 
            // globalData
            // 
            this.globalData.DataSetName = "GlobalData";
            this.globalData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sprAGCafeteriaBindingSource
            // 
            this.sprAGCafeteriaBindingSource.DataMember = "SprAGCafeteria";
            this.sprAGCafeteriaBindingSource.DataSource = this.globalData;
            // 
            // sprAGCafeteriaTableAdapter
            // 
            this.sprAGCafeteriaTableAdapter.ClearBeforeFill = true;
            // 
            // tokenDataGridViewTextBoxColumn
            // 
            this.tokenDataGridViewTextBoxColumn.DataPropertyName = "Token";
            this.tokenDataGridViewTextBoxColumn.HeaderText = "Token";
            this.tokenDataGridViewTextBoxColumn.Name = "tokenDataGridViewTextBoxColumn";
            this.tokenDataGridViewTextBoxColumn.ReadOnly = true;
            this.tokenDataGridViewTextBoxColumn.Width = 600;
            // 
            // cardNoDataGridViewTextBoxColumn
            // 
            this.cardNoDataGridViewTextBoxColumn.DataPropertyName = "CardNo";
            this.cardNoDataGridViewTextBoxColumn.HeaderText = "CardNo";
            this.cardNoDataGridViewTextBoxColumn.Name = "cardNoDataGridViewTextBoxColumn";
            this.cardNoDataGridViewTextBoxColumn.ReadOnly = true;
            this.cardNoDataGridViewTextBoxColumn.Width = 5;
            // 
            // Issue
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            this.Issue.DefaultCellStyle = dataGridViewCellStyle1;
            this.Issue.HeaderText = "Issue";
            this.Issue.Name = "Issue";
            this.Issue.Text = "Issue";
            this.Issue.ToolTipText = "Issue";
            this.Issue.UseColumnTextForButtonValue = true;
            // 
            // MealDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 840);
            this.Controls.Add(this.dgvDtls);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MealDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::. Meal Details";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDtls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.globalData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sprAGCafeteriaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDtls;
        private System.Windows.Forms.BindingSource sprAGCafeteriaBindingSource;
        private GlobalData globalData;
        private GlobalDataTableAdapters.SprAGCafeteriaTableAdapter sprAGCafeteriaTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn tokenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Issue;
    }
}