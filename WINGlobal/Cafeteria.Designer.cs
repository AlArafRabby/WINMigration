
namespace WINGlobal
{
    partial class Cafeteria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cafeteria));
            this.lblAppStatus = new System.Windows.Forms.Label();
            this.txtCardNo = new System.Windows.Forms.TextBox();
            this.logobx = new System.Windows.Forms.PictureBox();
            this.lblDevelop = new System.Windows.Forms.Label();
            this.lnkTodayMeal = new System.Windows.Forms.LinkLabel();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logobx)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppStatus
            // 
            this.lblAppStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblAppStatus.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAppStatus.Location = new System.Drawing.Point(2, 3);
            this.lblAppStatus.Name = "lblAppStatus";
            this.lblAppStatus.Size = new System.Drawing.Size(1265, 67);
            this.lblAppStatus.TabIndex = 111111137;
            this.lblAppStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCardNo
            // 
            this.txtCardNo.Location = new System.Drawing.Point(401, 33);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(169, 20);
            this.txtCardNo.TabIndex = 0;
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            // 
            // logobx
            // 
            this.logobx.BackColor = System.Drawing.Color.Transparent;
            this.logobx.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logobx.Image = global::WINGlobal.Properties.Resources.Ag_Logo;
            this.logobx.Location = new System.Drawing.Point(576, 73);
            this.logobx.Name = "logobx";
            this.logobx.Size = new System.Drawing.Size(183, 214);
            this.logobx.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logobx.TabIndex = 111111136;
            this.logobx.TabStop = false;
            // 
            // lblDevelop
            // 
            this.lblDevelop.BackColor = System.Drawing.Color.Transparent;
            this.lblDevelop.Font = new System.Drawing.Font("Verdana", 6.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevelop.ForeColor = System.Drawing.Color.Gray;
            this.lblDevelop.Location = new System.Drawing.Point(0, 598);
            this.lblDevelop.Name = "lblDevelop";
            this.lblDevelop.Size = new System.Drawing.Size(333, 21);
            this.lblDevelop.TabIndex = 111111135;
            this.lblDevelop.Text = "Developed By  Application Development Team, Akij Group";
            this.lblDevelop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnkTodayMeal
            // 
            this.lnkTodayMeal.AutoSize = true;
            this.lnkTodayMeal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTodayMeal.Location = new System.Drawing.Point(356, 605);
            this.lnkTodayMeal.Name = "lnkTodayMeal";
            this.lnkTodayMeal.Size = new System.Drawing.Size(133, 13);
            this.lnkTodayMeal.TabIndex = 111111138;
            this.lnkTodayMeal.TabStop = true;
            this.lnkTodayMeal.Text = "View Today meal ...";
            this.lnkTodayMeal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTodayMeal_LinkClicked);
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Verdana", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(1, 305);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(1266, 282);
            this.lblMsg.TabIndex = 111111140;
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cafeteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1266, 623);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lnkTodayMeal);
            this.Controls.Add(this.lblAppStatus);
            this.Controls.Add(this.txtCardNo);
            this.Controls.Add(this.logobx);
            this.Controls.Add(this.lblDevelop);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cafeteria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::. Akij House Cafeteria ";
            ((System.ComponentModel.ISupportInitialize)(this.logobx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppStatus;
        private System.Windows.Forms.TextBox txtCardNo;
        private System.Windows.Forms.PictureBox logobx;
        private System.Windows.Forms.Label lblDevelop;
        private System.Windows.Forms.LinkLabel lnkTodayMeal;
        private System.Windows.Forms.Label lblMsg;
    }
}