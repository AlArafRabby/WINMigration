
namespace AVL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MealDetails));
            this.dgvDtls = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDtls)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDtls
            // 
            this.dgvDtls.AllowUserToAddRows = false;
            this.dgvDtls.AllowUserToDeleteRows = false;
            this.dgvDtls.AllowUserToResizeRows = false;
            this.dgvDtls.BackgroundColor = System.Drawing.Color.White;
            this.dgvDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDtls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDtls.Location = new System.Drawing.Point(0, 0);
            this.dgvDtls.MultiSelect = false;
            this.dgvDtls.Name = "dgvDtls";
            this.dgvDtls.ReadOnly = true;
            this.dgvDtls.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDtls.ShowEditingIcon = false;
            this.dgvDtls.Size = new System.Drawing.Size(442, 573);
            this.dgvDtls.TabIndex = 1112;
            this.dgvDtls.TabStop = false;
            // 
            // MealDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 573);
            this.Controls.Add(this.dgvDtls);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MealDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::. Meal Details";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDtls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDtls;
    }
}