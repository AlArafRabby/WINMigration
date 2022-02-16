using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINGlobal
{
    public partial class Cafeteria : Form
    {
        DataTable dt = new DataTable(); GlobalClass bll = new GlobalClass(); string msg = "";
        string strCardNo; int lines; string tmpcrd = ""; int Count; char[] delimiterChars = { '^' };
        public Cafeteria() { InitializeComponent(); lblAppStatus.Text = "AKIJ HOUSE FOOD CORNER";}
        private void lnkTodayMeal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MealDetails dtl = new MealDetails(); dtl.ShowDialog();
            txtCardNo.Focus(); txtCardNo.BackColor = Color.Red;
        }      
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Count = 0; strCardNo = txtCardNo.Text; tmpcrd = strCardNo; lines = tmpcrd.Length;
                    if (strCardNo.Length >= 6 && strCardNo.Length <= 10)
                    {
                        dt = bll.GetCardCheck(strCardNo);
                        Count = int.Parse(dt.Rows[0]["Enroll"].ToString());
                        if (Count > 0)
                        {
                            #region -----------
                            dt = bll.GetCommonInformation(1, strCardNo);
                            if (dt.Rows.Count > 0) { msg = dt.Rows[0]["Token"].ToString(); }

                            string[] data = msg.Split(delimiterChars);
                            if (data[0] == "1")
                            {
                                msg = data[1] + Environment.NewLine + data[2];
                                lblMsg.Text = msg; lblMsg.ForeColor = Color.Navy;
                            }
                            else if (data[0] == "2")
                            {
                                msg = data[1] + Environment.NewLine + data[2];
                                lblMsg.Text = msg; lblMsg.ForeColor = Color.Red;
                            }
                            else if (data[0] == "3")
                            {
                                msg = data[1] + Environment.NewLine + data[2];
                                lblMsg.Text = msg; lblMsg.ForeColor = Color.Red;
                            }
                            #endregion
                            txtCardNo.Text = ""; lines = 0;
                        }
                        else { lblMsg.Text = "Opps! Your card is invalid."; lblMsg.ForeColor = Color.OrangeRed; txtCardNo.Text = ""; lines = 0; }
                    }
                }
                catch { lblMsg.Text ="[ Row Count: "+ Count.ToString() + " ] @ "+msg; lblMsg.Font= new Font("Arial", 14, FontStyle.Bold); lblMsg.ForeColor = Color.Red; }
            }
        }

    }
}
