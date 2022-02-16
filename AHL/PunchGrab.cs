using BioBridgeSDKLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace AHL
{
    public partial class PunchGrab : Form
    {
        public ZkemClient objZkeeper; BioBridgeSDK bbrg = new BioBridgeSDK(); DataTable dt; BLLClass bll = new BLLClass(); string xmlpath = ""; string date = "1984-06-17";
        int yr = 0; int mth = 0; int day = 0; int hr = 0; int min = 0; int sec = 0; int ver = 0; int io = 0; int work = 0; string strDBStatus;
        int enroll = 0; bool isinsert; int log = 0; string msg; string sl = "0"; string mip = ""; string mtp = ""; string location = "";
        public PunchGrab()
        {
            InitializeComponent();
        }
        private void Loadmachine()
        {
            try
            {
                btnStop.Enabled = false; listBox.Items.Clear();
               GetHeadQuaterCollection(); GetRemoteCollection(); /* GetCloudCollection();*/ /*  */
            }
            catch (Exception ex) { listBox.Items.Add(">> " + location + " " + ex.ToString()); }

            btnStop.Enabled = true; timer_apps.Enabled = true; timer_apps.Start();
            DateTime nextTime = DateTime.Now.AddMinutes(30);
            lblAppStatus.Text = "Next Run Time at:" + nextTime.Hour + ":" + nextTime.Minute + ":" + nextTime.Second;
        }
        private void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        ShowStatusBar("The device is switched off", true);
                        break;
                    }
                default:
                    break;
            }

        }
        public void ShowStatusBar(string message, bool type)
        {
            if (message.Trim() == string.Empty)
            {
                lblAppStatus.Visible = false;
                return;
            }
            lblAppStatus.Visible = true; lblAppStatus.Text = message;
            lblAppStatus.ForeColor = Color.White;

            if (type) lblAppStatus.BackColor = Color.FromArgb(79, 208, 154);
            else lblAppStatus.BackColor = Color.FromArgb(230, 112, 134);
        }
        private void btnStart_Click(object sender, EventArgs e)
        { lblAppStatus.Text = "Process Start ..."; btnStart.Enabled = false; btnStop.Enabled = true; Loadmachine(); }
        private void btnStop_Click(object sender, EventArgs e)
        {
            lblAppStatus.Text = "Process Stop......."; btnStart.Enabled = true; btnStop.Enabled = false;
            timer_apps.Stop(); timer_apps.Enabled = false;
        }
        private void timer_apps_Elapsed(object sender, ElapsedEventArgs e)
        { lblAppStatus.Text = "Start Running..."; timer_apps.Stop(); timer_apps.Enabled = false; Loadmachine(); }

        #region Remote Punch Collection ===============
        private void GetRemoteCollection()
        {
            try
            {
                dt = new DataTable();
                dt = bll.GetARLMachineList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sl = dt.Rows[i]["SL"].ToString();
                    mip = dt.Rows[i]["MIP"].ToString();
                    mtp = dt.Rows[i]["MTP"].ToString();
                    location = dt.Rows[i]["LOC"].ToString();
                    msg = GetRemoteCollection(sl, mip, mtp);
                    bll.UpdateMachineStatus(int.Parse(sl), 1, msg);
                    listBox.Items.Add(">> " + location + " " + msg);
                    sl = ""; mip = ""; mtp = ""; location = ""; msg = "";
                }
            }
            catch { }
        }
        public string GetRemoteCollection(string sl, string mip, string mtp)
        {
            try
            {
                if (mtp == "Face" || mtp == "Fingure")
                {
                    if (bbrg.Connect_TCPIP("R2", 1, mip, 4370, 0) == 0)
                    {
                        bbrg.GetDeviceTime(ref yr, ref mth, ref day, ref hr, ref min, ref sec);
                        date = yr.ToString() + "-" + mth.ToString() + "-" + day.ToString();
                        xmlpath = "D:/Deployment/XMLFiles/" + yr.ToString() + "-" + mth.ToString() + "-" + day.ToString() + "_AHLRemotePunch.xml";
                        strDBStatus = "DBFAIL";
                        if (bbrg.ReadGeneralLog(ref log) == 0)
                        {
                            if (mtp == "Face")
                            {
                                string strenroll = enroll.ToString(); isinsert = false;
                                do
                                {
                                    if (strenroll != "0" && strenroll != null && strenroll != "")
                                    {
                                        strDBStatus = bll.InsertRemotePunch(int.Parse(strenroll.Trim()), (mth + "/" + day + "/" + yr), (hr + ":" + min + ":" + sec), mip);
                                        CreateRemoteXml(strenroll.Trim(), (mth + "/" + day + "/" + yr), (hr + ":" + min + ":" + sec), mip);
                                        isinsert = true;
                                    }
                                } while (bbrg.SSR_GetGeneralLog(ref strenroll, ref yr, ref mth, ref day, ref hr, ref min, ref sec, ref ver, ref io, ref work) == 0);
                            }
                            else if (mtp == "Fingure")
                            {
                                isinsert = false;
                                do
                                {
                                    if (enroll > 0)
                                    {
                                        strDBStatus = bll.InsertRemotePunch(enroll, (mth + "/" + day + "/" + yr), (hr + ":" + min + ":" + sec), mip);
                                        CreateRemoteXml(enroll.ToString(), (mth + "/" + day + "/" + yr), (hr + ":" + min + ":" + sec), mip);
                                        isinsert = true;
                                    }
                                } while (bbrg.GetGeneralLog(ref enroll, ref yr, ref mth, ref day, ref hr, ref min, ref sec, ref ver, ref io, ref work) == 0);
                            }

                            if (isinsert == true && strDBStatus != "DBFAIL")
                            {
                                bbrg.DeleteGeneralLog();
                                msg = mip + "---> Data collection has been completed @ " + DateTime.Now.ToString();
                            }
                        }
                        else { msg = mip + "---> Sorry there is no data !!! @ " + DateTime.Now.ToString(); }
                    }
                    else { msg = mip + "---> Connection failed !!! @ " + DateTime.Now.ToString(); }
                }
                else if (mtp == "ZKT")
                {
                    objZkeeper = new ZkemClient(RaiseDeviceEvent); isinsert = false; strDBStatus = "DBFAIL"; string enrollNo = enroll.ToString();
                    bool isDeviceConnected = objZkeeper.Connect_Net(mip, 4370);
                    if (isDeviceConnected)
                    {
                        objZkeeper.ReadAllGLogData(int.Parse(sl));
                        while (objZkeeper.SSR_GetGeneralLogData(int.Parse(sl), out enrollNo, out ver, out io, out yr, out mth, out day, out hr, out min, out sec, ref work))
                        {
                            string inputDate = new DateTime(yr, mth, day, hr, min, sec).ToString();
                            int MachineNumber = int.Parse(sl); int IndRegID = int.Parse(enrollNo);
                            string DateTimeRecord = inputDate; string attDate = DateTime.Parse(inputDate).ToString("yyyy-MM-dd");
                            string attTime = DateTime.Parse(inputDate).ToString("HH:mm:ss");
                            strDBStatus = bll.InsertRemotePunch(IndRegID, attDate, attTime, mip);
                            isinsert = true;
                        }
                        if (isinsert == true && strDBStatus != "DBFAIL")
                        {
                            objZkeeper.ClearGLog(int.Parse(sl));
                            msg = mip + "---> Data collection has been completed @ " + DateTime.Now.ToString();
                        }
                        else { msg = mip + "---> Sorry there is no data !!! @ " + DateTime.Now.ToString(); }
                    }
                    else { msg = mip + "---> Connection failed !!! @ " + DateTime.Now.ToString(); }
                }

            }
            catch (Exception exp) { msg = exp.ToString() + DateTime.Now.ToString(); }
            return msg;
        }
        private void CreateRemoteXml(string EnrollNo, string MDate, string MTime, string Mip)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                if (System.IO.File.Exists(xmlpath))
                {
                    doc.Load(xmlpath);
                    XmlNode rootNode = doc.SelectSingleNode("Punch");
                    XmlNode addItem = CreateItemNode(doc, EnrollNo, MDate, MTime, Mip);
                    rootNode.AppendChild(addItem);
                }
                else
                {
                    XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                    doc.AppendChild(xmldeclerationNode);
                    XmlNode rootNode = doc.CreateElement("Punch");
                    XmlNode addItem = CreateItemNode(doc, EnrollNo, MDate, MTime, Mip);
                    rootNode.AppendChild(addItem);
                    doc.AppendChild(rootNode);
                }
                doc.Save(xmlpath);
            }
            catch { }
        }
        private XmlNode CreateItemNode(XmlDocument doc, string EnrollNo, string MDate, string MTime, string Mip)
        {
            XmlNode node = doc.CreateElement("PunchData");
            XmlAttribute Enroll = doc.CreateAttribute("EnrollNo");
            Enroll.Value = EnrollNo;
            XmlAttribute Date = doc.CreateAttribute("MDate");
            Date.Value = MDate;
            XmlAttribute Time = doc.CreateAttribute("MTime");
            Time.Value = MTime;
            XmlAttribute IP = doc.CreateAttribute("Mip");
            IP.Value = Mip;
            node.Attributes.Append(Enroll);
            node.Attributes.Append(Date);
            node.Attributes.Append(Time);
            node.Attributes.Append(IP);
            return node;
        }
        #endregion

        #region HeadQuater Punch Collection ===========
        private void GetHeadQuaterCollection()
        {
            try
            {
                dt = new DataTable();
                xmlpath = "D:/Deployment/XMLFiles/" + DateTime.Now.ToString("yyyy-MM-dd") + "_AHLHQPunch.xml"; //

                dt = bll.GetHQMachineList();
                if (dt.Rows.Count > 0)
                {
                    btnStop.Enabled = false; string msg = "";
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        //intValue AS MRID, dtTime AS AttDate, strBranch AS MID, strCode AS CardNo, strAccount AS MName
                        string mrid = dt.Rows[row]["LogID"].ToString();
                        string mid = dt.Rows[row]["ID1"].ToString();
                        string mname = dt.Rows[row]["ID1Data"].ToString();
                        string date = dt.Rows[row]["PanelDate"].ToString();
                        string cardno = dt.Rows[row]["ID2"].ToString();
                        string empname = dt.Rows[row]["ID2Data"].ToString();
                        CreateHQXml(mrid, mid, mname, date, cardno, empname);
                    }
                    #region------------ Insert into Database with XML Data ---------
                    XmlDocument doc = new XmlDocument(); doc.Load(xmlpath);
                    XmlNode vouchers = doc.SelectSingleNode("Punch");
                    string xmlString = vouchers.InnerXml;
                    xmlString = "<Punch>" + xmlString + "</Punch>";
                    msg = bll.InsertHQPunch(1, xmlString);//"XML Created Successfully...";// 
                    listBox.Items.Add(">> " + msg);
                    #endregion
                }
            }
            catch (Exception ex) { lblAppStatus.Text = ex.ToString(); }
        }
        private void CreateHQXml(string mrid, string mid, string mname, string date, string cardno, string empname)
        {
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(xmlpath))
            {
                doc.Load(xmlpath);
                XmlNode rootNode = doc.SelectSingleNode("Punch");
                XmlNode addItem = CreateItemNode(doc, mrid, mid, mname, date, cardno, empname);
                rootNode.AppendChild(addItem);
            }
            else
            {
                XmlNode xmldeclerationNode = doc.CreateXmlDeclaration("1.0", "", "");
                doc.AppendChild(xmldeclerationNode);
                XmlNode rootNode = doc.CreateElement("Punch");
                XmlNode addItem = CreateItemNode(doc, mrid, mid, mname, date, cardno, empname);
                rootNode.AppendChild(addItem);
                doc.AppendChild(rootNode);
            }
            doc.Save(xmlpath);
        }
        private XmlNode CreateItemNode(XmlDocument doc, string mrid, string mid, string mname, string date, string cardno, string empname)
        {
            XmlNode node = doc.CreateElement("PunchData");
            XmlAttribute Mrid = doc.CreateAttribute("mrid");
            Mrid.Value = mrid;
            XmlAttribute Mid = doc.CreateAttribute("mid");
            Mid.Value = mid;
            XmlAttribute Mname = doc.CreateAttribute("mname");
            Mname.Value = mname;
            XmlAttribute Date = doc.CreateAttribute("date");
            Date.Value = date;
            XmlAttribute Cardno = doc.CreateAttribute("cardno");
            Cardno.Value = cardno;
            XmlAttribute Empname = doc.CreateAttribute("empname");
            Empname.Value = empname;

            node.Attributes.Append(Mrid);
            node.Attributes.Append(Mid);
            node.Attributes.Append(Mname);
            node.Attributes.Append(Date);
            node.Attributes.Append(Cardno);
            node.Attributes.Append(Empname);
            return node;
        }

        #endregion

        #region Cloud Punch Collection ===========
        private void GetCloudCollection()
        {
            try
            {
                dt = new DataTable(); dt = bll.GetCloudMachineList();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    msg = "";
                    string rid = dt.Rows[i]["Rowid"].ToString();
                    string mid = dt.Rows[i]["MachineID"].ToString();
                    string loc = dt.Rows[i]["Location"].ToString();
                    string url = dt.Rows[i]["URLLink"].ToString();
                    string json = new WebClient().DownloadString(url);
                    msg = bll.InsertCloudPunch(int.Parse(rid), json);
                    listBox.Items.Add(msg + " " + loc + " on " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                }
            }
            catch (Exception ex) { msg = " ---> " + ex.ToString(); }
        }

        #endregion


    }
}
