using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARL
{
    public partial class Cafeteria : Form
    {
        DataTable dt = new DataTable(); BLLClass bll = new BLLClass(); string msg = "";
        string strCardNo; int lines; string tmpcrd = ""; int Count; char[] delimiterChars = { '^' };
        public Cafeteria()
        {
            InitializeComponent(); lblAppStatus.Text = "AKIJ HOUSE FOOD CORNER";
        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        
        {
            try
            {
                Count = 0;
                strCardNo = txtCardNo.Text;
                tmpcrd = strCardNo;
                lines = tmpcrd.Length;

                dt = bll.GetEnrollByCode(strCardNo);
                if (dt.Rows.Count > 0)
                {
                    Count = int.Parse(dt.Rows[0]["intEmployeeID"].ToString());
                }

                if (Count > 0)
                {
                    #region -----------
                    dt = bll.GetMealStatus(strCardNo);
                    if (dt.Rows.Count > 0)
                    {
                        msg = dt.Rows[0]["msg"].ToString();
                    }

                    string[] data = msg.Split(delimiterChars);
                    if (data[0] == "1")
                    {
                        msg = data[1] + Environment.NewLine + data[2];
                        txtMessage.Text = msg; txtMessage.ForeColor = Color.Navy;
                    }
                    else if (data[0] == "2")
                    {
                        msg = data[1] + Environment.NewLine + data[2];
                        txtMessage.Text = msg; txtMessage.ForeColor = Color.Red;
                    }
                    else if (data[0] == "3")
                    {
                        msg = data[1] + Environment.NewLine + data[2] + Environment.NewLine + data[3];
                        txtMessage.Text = msg; txtMessage.ForeColor = Color.Red;
                    }
                    #endregion

                    txtCardNo.Text = "";
                    lines = 0;

                }
                else { txtMessage.Text = ""; }
            }
            catch { txtMessage.Text = ""; }
        }

        private void lnkTodayMeal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MealDetails dtl = new MealDetails(); dtl.ShowDialog();
            txtCardNo.Focus();txtCardNo.BackColor = Color.Red;
        }
    }
}
