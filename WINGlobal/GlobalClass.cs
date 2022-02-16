using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WINGlobal.GlobalDataTableAdapters;

namespace WINGlobal
{
    public class GlobalClass
    {
        #region ============ Cafeteria Information Methods ============   
        internal DataTable GetCardCheck(string cardNo)
        {
            try
            {
                Int64 card = Convert.ToInt64(cardNo);
                CardValidationTableAdapter adp = new CardValidationTableAdapter();
                return adp.GetCardValidityData(card.ToString());
            }
            catch { return new DataTable(); }
        }
        internal DataTable GetCommonInformation(int part, string cardNo)
        {
            try
            {
                Int64 card = Convert.ToInt64(cardNo);
                SprAGCafeteriaTableAdapter adp = new SprAGCafeteriaTableAdapter();
                return adp.CommonInformationData(part, card.ToString());
            }
            catch { return new DataTable(); }
        }
        
        #endregion

        #region========== DashBoard Information Methods==============
        internal string UpdateNews(string newnews, int actionBy)
        {
            string msg = "Update Successfully";
            try
            {
                TblAppsNewsTableAdapter adp = new TblAppsNewsTableAdapter();
                adp.SetUpdateData(newnews, actionBy);
            }
            catch (Exception ex) { msg = ex.ToString(); }
            return msg;
        }
        internal DataTable GetUserEnrollCheck(int v)
        {
            return new DataTable();
        }
        internal DataTable GetEmpInfo(string strCardNo)
        {
            return new DataTable();
        }
        internal DataTable GetNews()
        {            
            try
            {
                TblAppsNewsTableAdapter adp = new TblAppsNewsTableAdapter();
                return adp.GetAppsNews();
            }
            catch { return new DataTable(); }
        }
        internal DataTable GetEmpCheck(string strCardNo)
        {
            return new DataTable();
        }
        internal DataTable GetAppPermission(int v)
        {
            return new DataTable();
        }
        #endregion




    }
}
