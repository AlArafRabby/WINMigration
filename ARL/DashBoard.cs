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
    public partial class DashBoard : Form
    {
        int intCardNo, intCheckval, intPermission, intUserCount, intCount;
        string strCardNo; Int64 intCard; int Count;
        DataTable dt = new DataTable(); BLLClass obj = new BLLClass();

        public DashBoard()
        {
            InitializeComponent();
            lblHeader.Text = "আপনার আইডি কার্ড পাঞ্চ করুন।";
            lblUserEnroll.Visible = false;
            txtUserEnroll.Visible = false;
            chkAdministrator.Checked = false;
            dt = obj.GetNews();
            rtxtNews.Text = dt.Rows[0]["strNews"].ToString();
            rtxNewsCheck();
        }
        private void rtxNewsCheck()
        {
            rtxtNews.ReadOnly = true;
            btnNewsUpdate.Visible = false;

            if (rtxtNews.Text == "")
            {
                rtxtNews.Visible = false;
                lblNews.Visible = false;
            }
        }
        private void chkAdministrator_CheckedChanged(object sender, EventArgs e)
        {
            txtCardNo.Focus();
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Count = 0;
                    intCard = Convert.ToInt64(txtCardNo.Text.ToString());
                    strCardNo = intCard.ToString();
                    dt = obj.GetEnrollByCode(strCardNo);
                    if (dt.Rows.Count > 0)
                    {
                        Count = int.Parse(dt.Rows[0]["intEmployeeID"].ToString());
                    }

                    if (Count > 0)
                    {
                        dt = obj.GetEmpCheck(strCardNo);
                        intCheckval = int.Parse(dt.Rows[0]["intVal"].ToString());

                        if (intCheckval == 1)
                        {
                            dt = obj.GetEmpInfo(strCardNo);

                            txtEnroll.Text = dt.Rows[0]["intEmployeeID"].ToString();
                            txtEmpName.Text = dt.Rows[0]["strEmployeeName"].ToString();
                            txtDesig.Text = dt.Rows[0]["strDesignation"].ToString();
                            txtDept.Text = dt.Rows[0]["strDepatrment"].ToString();
                            txtJobType.Text = dt.Rows[0]["strJobType"].ToString();
                            txtUnit.Text = dt.Rows[0]["strUnit"].ToString();
                            txtMobile.Text = dt.Rows[0]["strContactNo1"].ToString();
                            txtEmpCode.Text = dt.Rows[0]["strEmployeeCode"].ToString();

                            dt = obj.GetAppPermission(int.Parse(txtEnroll.Text));
                            intPermission = int.Parse(dt.Rows[0]["intCount"].ToString());

                            if (chkAdministrator.Checked == true && intPermission == 1)
                            {
                                lblNews.Visible = true;
                                rtxtNews.Visible = true;
                                rtxtNews.ReadOnly = false;
                                btnNewsUpdate.Visible = true;

                                txtCardNo.Enabled = false;
                                lblHeader.Text = "ইনরোল প্রদান করুন।";
                                pboxReset.Visible = true;
                                lblUserEnroll.Visible = true;
                                txtUserEnroll.Visible = true;
                                txtUserEnroll.Select();
                                txtUserEnroll.Focus();
                            }
                            else
                            {
                                txtUserEnroll.Text = txtEnroll.Text;
                                rtxNewsCheck();
                                txtCardNoEnter_Click();
                            }
                        }
                        else
                        {
                            MessageBox.Show("আপনার কার্ড নাম্বারটি আপডেট নয়।");
                            txtCardNo.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("আপনার কার্ড নাম্বারটি আকিজ রিসোর্সের জন্য অনুমোদিত নয়।");
                        txtCardNo.Text = "";
                    }
                }
                catch { }
            }
        }

        private void txtCardNoEnter_Click()
        {
            try
            {
                this.Hide();
                DashBoardCardInfo frm = new DashBoardCardInfo(txtEnroll.Text, txtEmpCode.Text, txtEmpName.Text, txtDesig.Text, txtDept.Text, txtJobType.Text, txtUnit.Text, txtMobile.Text, txtUserEnroll.Text);
                frm.Show();
            }
            catch { }
        }

        private void txtUserEnroll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtUserEnroll.Text == "")
                    {
                        txtUserEnroll.Text = txtEnroll.Text;
                        txtCardNoEnter_Click();
                    }
                    else
                    {
                        dt = obj.GetUserEnrollCheck(int.Parse(txtUserEnroll.Text));
                        intCount = int.Parse(dt.Rows[0]["intCount"].ToString());
                        if (intCount == 1)
                        {
                            txtCardNoEnter_Click();
                        }
                        else
                        {
                            MessageBox.Show("আপনি যে ইনরোল দিয়েছেন তা সঠিক নয়।");
                        }

                    }
                }
                catch { }
            }
        }

        private void txtUserEnroll_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtUserEnroll.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtUserEnroll.Text = txtUserEnroll.Text.Remove(txtUserEnroll.Text.Length - 1);
            }
        }

        private void pboxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pboxReset_Click(object sender, EventArgs e)
        {
            rtxNewsCheck();
            txtCardNo.Text = "";
            txtEnroll.Text = "";
            txtEmpName.Text = "";
            txtDesig.Text = "";
            txtDept.Text = "";
            txtJobType.Text = "";
            txtUnit.Text = "";
            txtMobile.Text = "";
            txtEmpCode.Text = "";
            txtUserEnroll.Text = "";
            txtCardNo.Enabled = true;
            lblUserEnroll.Visible = false;
            txtUserEnroll.Visible = false;
            chkAdministrator.Checked = false;
            pboxReset.Visible = false;
            txtCardNo.Select();
            txtCardNo.Focus();
            lblHeader.Text = "আপনার আইডি কার্ড পাঞ্চ করুন।";

        }

        private void btnNewsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string strNews = rtxtNews.Text;
                int intActionBy = int.Parse(txtEnroll.Text);
                obj.UpdateNews(strNews, intActionBy);
            }
            catch { }
        }






    }
}
