using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ARL
{
    public partial class EmailSend : Form
    {
        DataTable dt = new DataTable(); BLLClass bll = new BLLClass(); string msg = "";
        public EmailSend() { InitializeComponent(); }
        private void btnStart_Click(object sender, EventArgs e)
        {
            lblAppStatus.Text = "Process Start ...";
            btnStart.Enabled = false; btnStop.Enabled = true;
            SendEmailData();
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            lblAppStatus.Text = "Process Stop.......";
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            timer_apps.Stop();
            timer_apps.Enabled = false;
        }
        private void timer_apps_Elapsed(object sender, ElapsedEventArgs e)
        {
            lblAppStatus.Text = "Start Running...";
            timer_apps.Stop(); timer_apps.Enabled = false;
            SendEmailData();
        }
        private void SendEmailData()
        {
            try
            {
                btnStop.Enabled = false;
                msg = bll.InsertHQPunch(3, "");
                listBox.Items.Add(">> " + msg);
                msg = bll.InsertHQPunch(6, "");
                listBox.Items.Add(">> " + msg);

                btnStop.Enabled = true; timer_apps.Enabled = true; timer_apps.Start();
                DateTime nextTime = DateTime.Now.AddMinutes(30);
                lblAppStatus.Text = "Next Run Time at:" + nextTime.Hour + ":" + nextTime.Minute + ":" + nextTime.Second;
            }
            catch (Exception ex) { listBox.Items.Add(">> " + ex.ToString()); }
        }
    }
}
