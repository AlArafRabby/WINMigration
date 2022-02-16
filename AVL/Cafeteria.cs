using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVL
{
    public partial class Cafeteria : Form
    {
        DataTable dt; BLLClass bll = new BLLClass(); string msg = ""; string tmpcrd = "";
        string strCardNo; int lines; int Count; char[] delimiterChars = { '^' };
        public Cafeteria() {InitializeComponent(); lblAppStatus.Text = "AKIJ HOUSE FOOD CORNER"; txtCardNo.Focus(); }

        private void lnkTodayMeal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MealDetails dtl = new MealDetails(); dtl.ShowDialog();
            txtCardNo.Focus();txtCardNo.BackColor = Color.Red;
        }
        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Count = 0; lblMsg.Text = ""; strCardNo = txtCardNo.Text;
                tmpcrd = strCardNo; lines = tmpcrd.Length;dt = new DataTable(); 
                if (strCardNo.Length >= 10) { 
                    dt = bll.GetEnrollByCode(strCardNo); 
                    Count = int.Parse(dt.Rows[0]["Enroll"].ToString()); }

                if (Count > 0)
                {
                    #region -----------
                    dt = new DataTable(); dt = bll.GetMealStatus(strCardNo);
                    if (dt.Rows.Count > 0)
                    {
                        msg = dt.Rows[0]["msg"].ToString();
                    }

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
                        msg = data[1] + Environment.NewLine + data[2] + Environment.NewLine + data[3];
                        lblMsg.Text = msg; lblMsg.ForeColor = Color.Red;

                    }
                    #endregion
                    txtCardNo.Text = ""; lines = 0;
                }
                else { lblMsg.Text = "Your card is invalid for AKIJ VENTURE LTD. "; } //z
            }
            catch (Exception ex) { lblMsg.Text = ex.ToString(); }
        }

    }
}
