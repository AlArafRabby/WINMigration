using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ARL
{
    public partial class SMSSend : Form
    {
        DataTable dt = new DataTable(); BLLClass bll = new BLLClass(); string msg = "";
        public SMSSend() { InitializeComponent(); }
        private void btnStart_Click(object sender, EventArgs e)
        {
            lblAppStatus.Text = "Process Start ..."; btnStart.Enabled = false; btnStop.Enabled = true;
            SendPendingSMS(); btnStop.Enabled = true; timer_apps.Enabled = true; timer_apps.Start();
            DateTime nextTime = DateTime.Now.AddSeconds(7);
            lblAppStatus.Text = "Next Run Time at:" + nextTime.Hour + ":" + nextTime.Minute + ":" + nextTime.Second;
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            lblAppStatus.Text = "Process Stop.......";
            btnStart.Enabled = true; btnStop.Enabled = false; timer_apps.Stop();
            timer_apps.Enabled = false;
        }
        private void timer_apps_Elapsed(object sender, ElapsedEventArgs e)
        {
            lblAppStatus.Text = "Start Running...";
            timer_apps.Stop(); timer_apps.Enabled = false; SendPendingSMS();
            btnStop.Enabled = true; timer_apps.Enabled = true; timer_apps.Start();
            DateTime nextTime = DateTime.Now.AddSeconds(7);
            lblAppStatus.Text = "Next Run Time at:" + nextTime.Hour + ":" + nextTime.Minute + ":" + nextTime.Second;
        }
        private void SendPendingSMS()
        {
            try
            {
                btnStop.Enabled = false; dt = bll.GetUrlList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    msg = "";
                    string id = dt.Rows[i]["intID"].ToString();
                    string indentity = dt.Rows[i]["Identification"].ToString();
                    string url = dt.Rows[i]["strSMS"].ToString();
                    string status = CallWebService(url);
                    msg = indentity + bll.UpdateSendList(id, status);
                    listBox.Items.Add(">> " + msg);
                }
            }
            catch (Exception ex) { msg = " ---> " + ex.ToString(); }
        }
        private string CallWebService(string requestURL)
        {
            string response = "";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                WebRequest myWebRequest = WebRequest.Create(requestURL);
                WebResponse myWebResponse = myWebRequest.GetResponse();
                Stream streamResponse = myWebResponse.GetResponseStream();
                StreamReader reader = new StreamReader(streamResponse);
                response = reader.ReadToEnd();
                reader.Close();
                streamResponse.Close();
                myWebResponse.Close();
            }
            catch (Exception ex)
            {
                response = ex.ToString();
            }
            return response;
        }
        



















    }
}
