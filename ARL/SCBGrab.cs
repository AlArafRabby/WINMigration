using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace ARL
{
    public partial class SCBGrab : Form
    {
        DataTable dt = new DataTable(); BLLClass bll = new BLLClass(); string msg = "";
        string validExtensions = "*.rpt"; string brnch; decimal opBaln; decimal clBaln; string accno;
        public SCBGrab()
        {
            InitializeComponent();
        }
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
        private string DateFormet_IBBL(DateTime date)
        {
            string day = date.Day >= 10 ? date.Day.ToString() : ("0" + date.Day.ToString());
            string month = date.Month >= 10 ? date.Month.ToString() : ("0" + date.Month.ToString());
            string year = date.Year.ToString(); return year + month + day + "000000";
        }
        private DateTime GetStartDate_IBBL(string accountNumberID, ref decimal? runningBalance)
        {
            DateTime? dteDate = DateTime.Now;
            bll.GetLastCollectionDateAndRuningBalance(int.Parse(accountNumberID), ref dteDate, ref runningBalance);
            return dteDate.Value;
        }
        private void GetPendingBankData()
        {
            try
            {
                btnStop.Enabled = false; listBox.Items.Clear(); GetXMLFiles();
            }
            catch (Exception ex) { msg = ">> " + ex.ToString(); listBox.Items.Add(">> " + msg + " on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")); }
            btnStop.Enabled = true; timer_apps.Enabled = true; timer_apps.Start();
            DateTime nextTime = DateTime.Now.AddHours(12);
            lblAppStatus.Text = "Next Run Time at:" + nextTime.Hour + ":" + nextTime.Minute + ":" + nextTime.Second;
        }
        private void GetXMLFiles()
        {
            try
            {
                #region ============Report File Count In Directory ============                
                string flocation = "D:\\SCBH2H\\host2host\\documents\\icas\\in\\rpt";
                string mlocation = "D:\\SCBH2H\\host2host\\documents\\icas\\in\\rpt\\MoveData";
                DirectoryInfo movdir = new DirectoryInfo(mlocation);
                string[] extFilter = validExtensions.Split(new char[] { ',' });
                ArrayList files = new ArrayList();
                DirectoryInfo dirInfo = new DirectoryInfo(flocation);
                foreach (string extension in extFilter) { files.AddRange(dirInfo.GetFiles(extension)); }

                for (int flNo = 0; flNo < files.Count; flNo++)
                {
                    try
                    {
                        ArrayList typelst = new ArrayList(); ArrayList arrlst = new ArrayList();
                        XmlDocument doc = new XmlDocument(); brnch = ""; opBaln = 0; clBaln = 0; accno = ""; msg = "";
                        doc.Load(flocation + "\\" + files[flNo]);
                        string innerXML = doc.InnerXml;
                        innerXML = innerXML.Substring(innerXML.IndexOf("<BkToCstmrStmtV01>"));
                        innerXML = innerXML.Replace("</Document>", "");
                        doc.InnerXml = innerXML;
                        foreach (System.Xml.XmlNode ndProcessingBranch in doc.SelectNodes("BkToCstmrStmtV01/Stmt/Acct/Svcr/BrnchId/Id"))
                        { brnch = ndProcessingBranch.InnerText; }
                        foreach (System.Xml.XmlNode ndAccno in doc.SelectNodes("BkToCstmrStmtV01/Stmt/Acct/Id/PrtryAcct/Id"))
                        { accno = ndAccno.InnerText; }
                        foreach (System.Xml.XmlNode ndOpeningClosingbal in doc.SelectNodes("BkToCstmrStmtV01/Stmt/Bal/Amt"))
                        { arrlst.Add(ndOpeningClosingbal.InnerText); }
                        foreach (System.Xml.XmlNode ndBalType in doc.SelectNodes("BkToCstmrStmtV01/Stmt/Bal/CdtDbtInd"))
                        { typelst.Add(ndBalType.InnerText); }
                        if (typelst[1].ToString() == "DBIT") { opBaln = -(decimal.Parse(arrlst[1].ToString())); }
                        else { opBaln = decimal.Parse(arrlst[1].ToString()); }
                        if (typelst[3].ToString() == "DBIT") { clBaln = -(decimal.Parse(arrlst[3].ToString())); }
                        else { clBaln = decimal.Parse(arrlst[3].ToString()); }

                        bll.SCBBankStatement(accno, "", opBaln, "", 0, ref msg);
                        if (msg == "True")
                        {
                            foreach (System.Xml.XmlNode node in doc.SelectNodes("BkToCstmrStmtV01/Stmt/Ntry"))
                            {
                                innerXML = node.InnerXml;
                                bll.SCBBankStatement(accno, brnch, clBaln, innerXML, 1, ref msg);
                            }
                            msg = accno + " >> " + msg;
                            File.Move(flocation + "\\" + files[flNo], mlocation + "\\" + files[flNo]); File.Delete(flocation + "\\" + files[flNo]);
                        }
                        else { msg = accno + " >> " + msg; }
                    }
                    catch (Exception ex) { msg = accno + " >> " + "[ " + files[flNo] + " ]" + ex.ToString(); }
                    listBox.Items.Add(">> " + msg + " on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                #endregion
            }
            catch (Exception exp)
            { msg = " ---> " + exp.ToString(); listBox.Items.Add(">> " + msg + " on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")); }

        }















    }
}
