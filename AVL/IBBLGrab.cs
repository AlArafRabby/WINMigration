using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Windows.Forms;

namespace AVL
{
    public partial class IBBLGrab : Form
    {
        DataTable dt = new DataTable(); BLLClass bll = new BLLClass(); string msg = "";
        public IBBLGrab() { InitializeComponent(); }
        private void btnStart_Click(object sender, EventArgs e)
        {
            lblAppStatus.Text = "Process Start ..."; btnStart.Enabled = false; btnStop.Enabled = true;
            GetPendingBankData();
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
            timer_apps.Stop(); timer_apps.Enabled = false; GetPendingBankData();
        }
        private void GetPendingBankData()
        {
            try
            {
                btnStop.Enabled = false; listBox.Items.Clear();
                #region ============= Get ID and Login ============
                string userID = ""; string passCode = ""; int id = 0; string strRequestUrl = "";
                string strResponse = ""; string transID = ""; decimal? runningBalance = 0;
                DateTime? startDate; DateTime? endDate; string accountNumber = "";
                string parseInfo = ""; string msgTmp = ""; string pCode = "";
                string[] loginInfo = bll.GetLoginIDPassword("IBBL");
                userID = loginInfo[1]; passCode = loginInfo[2]; id = int.Parse(loginInfo[0]);
                string[][] accountNumbers = bll.Get_IBBL_AccountNumbers(id);
                #endregion

                for (int i = 0; i < accountNumbers.Length; i++)
                {
                    msg = ""; runningBalance = 0; pCode = "";
                    startDate = GetStartDate_IBBL(accountNumbers[i][1], ref runningBalance); endDate = DateTime.Now;
                    accountNumber = accountNumbers[i][0];

                    #region ========== Call Webservice for Login and Collect Transaction ID From IBBL =========
                    strRequestUrl = "https://ibblportal.islamibankbd.com/wsTransactionRequest.do?username=" + userID + "&passcode=" + passCode + "&transtype=101";
                    strResponse = CallWebService(strRequestUrl);
                    if (strResponse != "")
                    {
                        transID = bll.Parser_LoginXML(strResponse, ref pCode);
                        transID = HttpUtility.UrlEncode(transID);
                    }
                    #endregion

                    #region ============== Call DataCollection Webservice for all IBBL Accounts ==========

                    if (transID != "")
                    {
                        strRequestUrl = "https://ibblportal.islamibankbd.com/wsConfirmTransaction.do?transID=" + transID + "&startDate=" + DateFormet_IBBL(startDate.Value) + "&endDate=" + DateFormet_IBBL(endDate.Value) + "&accNo=" + accountNumber;
                        strResponse = CallWebService(strRequestUrl);
                        if (strResponse != "") { parseInfo = bll.Parser_XML(strResponse, runningBalance.Value, ref pCode); }
                        else { pCode = "01"; }
                        if (pCode == "11") // Go for DB insertion
                        {
                            bll.InsertBankStatement(transID, accountNumbers[i][0], int.Parse(accountNumbers[i][1]), startDate.Value, endDate.Value, strResponse, parseInfo, ref msgTmp);
                            msg = accountNumber + "--->" + msgTmp + ". ";
                        }
                        else if (pCode == "00") // Make a message and Insert log 
                        { msg += accountNumber + "--->No New Data For this Account. "; }
                        else if (pCode == "01")
                        { msg += accountNumber + "--->Do not get any data from IBBL. "; }
                        else if (pCode == "10")
                        { msg += accountNumber + "--->Pasing Error. "; }

                    }
                    else { msg = accountNumber + "--->" + (pCode == "" ? "IBBL Connection Error. " : pCode) + ". "; }
                    #endregion

                    listBox.Items.Add(">> " + msg + " on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                }
            }
            catch (Exception ex) { msg = " ---> " + ex.ToString(); }
            btnStop.Enabled = true; timer_apps.Enabled = true; timer_apps.Start();
            DateTime nextTime = DateTime.Now.AddMinutes(2);
            lblAppStatus.Text = "Next Run Time at:" + nextTime.Hour + ":" + nextTime.Minute + ":" + nextTime.Second;
        }
        private DateTime GetStartDate_IBBL(string accountNumberID, ref decimal? runningBalance)
        {
            DateTime? dteDate = DateTime.Now;
            bll.GetLastCollectionDateAndRuningBalance(int.Parse(accountNumberID), ref dteDate, ref runningBalance);
            return dteDate.Value;
        }
        private string CallWebService(string requestURL)
        {
            string responseFromServer = "";
            try
            {
                WebRequest myWebRequest = WebRequest.Create(requestURL);
                //myWebRequest.Proxy = GetProxyConfig();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
                WebResponse myWebResponse = myWebRequest.GetResponse();
                Stream streamResponse = myWebResponse.GetResponseStream();
                StreamReader reader = new StreamReader(streamResponse);
                responseFromServer = reader.ReadToEnd();
                reader.Close(); streamResponse.Close(); myWebResponse.Close();
            }
            catch { responseFromServer = ""; }
            return responseFromServer;
        }
        private WebProxy GetProxyConfig()
        {
            WebProxy myProxy = new WebProxy();
            Uri proxyUrl = new Uri(ConfigurationManager.AppSettings["pUrl"]);
            NetworkCredential credential = new NetworkCredential();
            credential.UserName = ConfigurationManager.AppSettings["userName"];
            credential.Password = ConfigurationManager.AppSettings["passCode"];
            myProxy.Address = proxyUrl;
            myProxy.Credentials = credential;
            return myProxy;
        }
        private string DateFormet_IBBL(DateTime date)
        {
            string day = date.Day >= 10 ? date.Day.ToString() : ("0" + date.Day.ToString());
            string month = date.Month >= 10 ? date.Month.ToString() : ("0" + date.Month.ToString());
            string year = date.Year.ToString(); return year + month + day + "000000";
        }

    }
}
