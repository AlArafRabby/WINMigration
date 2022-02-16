using AIL.DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AIL
{
    public class BLLClass
    {
        #region =============All Punch Collection Methods
        internal DataTable GetHQMachineList()
        {
            try
            {
                EventLogTableAdapter adp = new EventLogTableAdapter();
                return adp.GetHQMachineListData();
            }
            catch (Exception ex) { return new DataTable(); }
        }
        internal string InsertHQPunch(int part, string xmlString)
        {
            string msg = "";
            try
            {
                SprAHAccessControlTableAdapter adp = new SprAHAccessControlTableAdapter();
                adp.InsertHQPunchData(part, xmlString, ref msg);
            }
            catch (Exception ex) { msg = ex.ToString(); }
            return msg;
        }
        internal DataTable GetARLMachineList()
        {
            TblPunchMachineConfigTableAdapter adp = new TblPunchMachineConfigTableAdapter();
            try { return adp.GetMachineListData(); }
            catch { return new DataTable(); }
        }
        internal string InsertRemotePunch(int enrollNo, string date, string time, string ipAddress)
        {
            string strDBStatus = "DBFAIL"; SprEmployeePunchGraphTableAdapter adp = new SprEmployeePunchGraphTableAdapter();
            try { adp.InsertRemotePunchData(enrollNo, date, time, ipAddress, ref strDBStatus); } catch { }
            return strDBStatus;
        }
        internal void UpdateMachineStatus(int sl, int sts, string msg)
        {
            try { QueriesTableAdapter adp = new QueriesTableAdapter(); adp.UpdateMIPStatusQuery(msg, sts, sl); }
            catch { }
        }
        internal DataTable GetCloudMachineList()
        {
            try
            {
                TblCloudGrabingTableAdapter adp = new TblCloudGrabingTableAdapter();
                return adp.GetCMachineListData();
            }
            catch 
            { 
                return new DataTable(); 
            }
        }
        internal string InsertCloudPunch(int sl, string json)
        {
            string msg = "";
            try
            {
                SprGetCloudPunchTableAdapter adp = new SprGetCloudPunchTableAdapter();
                adp.InsertCloudPunchData(sl, json, ref msg);
            }
            catch (Exception ex) { msg = ex.ToString(); }
            return msg;
        }

        #endregion


        #region =============All Bank Collection Methods

        string[][] accNumbers; string[] id_passCode = new string[3];
        internal string[] GetLoginIDPassword(string bnk)
        {
            try
            {
                TblBankAccountStatementCollectorInfoTableAdapter adp = new TblBankAccountStatementCollectorInfoTableAdapter();
                DataSet.TblBankAccountStatementCollectorInfoDataTable tbl = adp.GetIBBLUserData(bnk);
                if (tbl.Rows.Count > 0)
                {
                    id_passCode[0] = tbl[0].intID.ToString();
                    id_passCode[1] = tbl[0].strUserID;
                    id_passCode[2] = tbl[0].strPassword;
                }
            }
            catch { }
            return id_passCode;
        }
        internal string[][] Get_IBBL_AccountNumbers(int id)
        {
            try
            {
                TblBankAccountStatementCollectorInfoDetailsTableAdapter adp = new TblBankAccountStatementCollectorInfoDetailsTableAdapter();
                DataSet.TblBankAccountStatementCollectorInfoDetailsDataTable tbl = adp.GetIBBLUserInfoData(id);
                accNumbers = new string[tbl.Rows.Count][];
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    accNumbers[i] = new string[2];
                    accNumbers[i][0] = tbl[i].strSiteKey;
                    accNumbers[i][1] = tbl[i].intAccountID.ToString();
                }
            }
            catch { }
            return accNumbers;
        }
        internal void GetLastCollectionDateAndRuningBalance(int accountID, ref DateTime? lastCollectionDate, ref decimal? runningBalance)
        {
            try
            {
                SprBankAccountStatementLatestDateTableAdapter adp = new SprBankAccountStatementLatestDateTableAdapter();
                adp.GetLastCollectionDateAndRunningBalanceData(accountID, ref lastCollectionDate, ref runningBalance);
            }
            catch { }
        }
        internal void InsertBankStatement(string strIbbl_TransID, string strAccountNo, int intAccID, DateTime dteFromDate, DateTime dteToDatte, string responseXml, string statementXml, ref string strResponse)
        {
            SprBankAccountStatementAddMainTableAdapter adp = new SprBankAccountStatementAddMainTableAdapter();
            try
            { adp.InsertStatementData(strIbbl_TransID, strAccountNo, intAccID, dteFromDate, dteToDatte, responseXml, statementXml, ref strResponse); }
            catch { strResponse = " Network problem"; }
        }
        internal void SCBBankStatement(string accno, string branch, decimal opBaln, string xmlstring, int type, ref string msg)
        {
            SprSCBStatementReconciliationTableAdapter adp = new SprSCBStatementReconciliationTableAdapter();
            try { adp.InsertSCBStatementData(type, branch, accno, opBaln, xmlstring, ref msg); }
            catch (Exception ex) { msg = ex.ToString(); }
        }
        //=======================================================
        internal DateTime GetDateAtSQLDateFormat(string dateString)
        {
            if ("" + dateString == "") return DateTime.Now;

            DateTimeFormatInfo dtf = new DateTimeFormatInfo();
            dtf.ShortDatePattern = "dd/MM/yyyy hh:mm tt";
            return Convert.ToDateTime(dateString, dtf);
        }
        internal string Parser_XML(string xml, decimal runningBalance, ref string parseCode)
        {
            int i = 0;
            int dataIndex = 0;
            bool ysnOk = false;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            if (GetTransactionStatus(doc))
            {
                XmlNodeList itemNodeList = doc.SelectNodes("/WebServiceEnvelop/statement/transaction");
                string[][] values = null;
                try
                {

                    foreach (XmlNode xn in itemNodeList)
                    {
                        if (xn["particulars"].InnerText == "B/F" || !ysnOk)
                        {
                            if (decimal.Parse(xn["balance"].InnerText) != runningBalance)
                            {
                                ysnOk = false;

                            }
                            else
                            {
                                ysnOk = true;
                                values = new string[itemNodeList.Count - (i + 1)][];
                            }
                        }
                        else
                        {


                            values[dataIndex] = new string[7];
                            values[dataIndex][0] = xn["accNo"].InnerText; //Acc
                            values[dataIndex][1] = GetDateAtSQLDateFormat(xn["transDate"].InnerText).ToShortDateString(); //DateSt
                            values[dataIndex][2] = xn["particulars"].InnerText; //Par
                            values[dataIndex][3] = xn["instrNo"].InnerText; //Chq
                            values[dataIndex][4] = xn["drAmt"].InnerText; //Dr
                            values[dataIndex][5] = xn["crAmt"].InnerText; //Cr
                            values[dataIndex][6] = xn["balance"].InnerText; //Run
                            dataIndex++;

                        }

                        i++;
                    }
                }
                catch
                {
                    parseCode = "10";
                    return "";
                }


                if (ysnOk && values.Length > 0)
                {
                    string strXML = GetXML(values);
                    parseCode = "11";
                    return strXML;
                }
                else
                {
                    parseCode = "00";
                    return "";
                }
            }
            else
            {
                parseCode = "01";
                return "";
            }



        }
        internal string Parser_LoginXML(string xml, ref string description)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            string transactionID = "";

            if (GetTransactionStatus(doc))
            {

                XmlNode node = doc.SelectSingleNode("/WebServiceEnvelop/transRequest/transID");
                transactionID = node.InnerText;
            }
            else
            {
                XmlNode node = doc.SelectSingleNode("/WebServiceEnvelop/description");
                description = node.InnerText;
            }

            return transactionID;
        }
        internal bool GetTransactionStatus(XmlDocument doc)
        {
            XmlNode node = doc.SelectSingleNode("/WebServiceEnvelop/status");


            if (node.InnerText != "99")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        string[] itemNames = { "Acc", "DateSt", "Par", "Chq", "Dr", "Cr", "Run" };
        string mainNodeName = "node", subNodeName = "item";
        internal string GetXML(string[][] itemvalue)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode mainNode = doc.CreateElement(mainNodeName);
            XmlNode subNode;
            for (int i = 0; i < itemvalue.Length; i++)
            {

                subNode = CreateNodeForItem(doc, itemvalue[i]);
                mainNode.AppendChild(subNode);
            }

            doc.AppendChild(mainNode);

            return doc.InnerXml;

        }
        internal XmlNode CreateNodeForItem(XmlDocument xmlDoc, string[] items)
        {
            XmlNode node = xmlDoc.CreateElement(subNodeName);
            XmlAttribute attr;
            for (int i = 0; i < items.Length; i++)
            {
                attr = xmlDoc.CreateAttribute(itemNames[i]);
                attr.Value = items[i];
                node.Attributes.Append(attr);
            }
            return node;
        }
        #endregion


        #region ===========SMS Send Methods
        internal DataTable GetUrlList()
        {
            try
            {
                TblAPIsmsTableAdapter adp = new TblAPIsmsTableAdapter();
                return adp.GetURLToSendSMSData();
            }
            catch { return new DataTable(); }
        }
        internal string UpdateSendList(string id, string status)
        {
            string msg = "";
            try
            {
                QueriesTableAdapter adp = new QueriesTableAdapter();
                adp.UpdateSendStatus(status, id);
                adp.ForRetrySend();
                msg = " ---> Message has been Send.";
            }
            catch (Exception ex) { msg = " ---> " + ex.ToString(); }
            return msg;
        }
        #endregion

    }
}
