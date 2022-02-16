using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINGlobal
{
    public partial class MealDetails : Form
    {
        string CardNo;
        public MealDetails()
        {
            InitializeComponent();
            DataTable dt = new DataTable(); GlobalClass bll = new GlobalClass();
            dt = bll.GetCommonInformation(2, "2");
            dgvDtls.DataSource = dt;
        }
        private void dgvDtls_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int Cindex = Convert.ToInt32(e.ColumnIndex);

            if (index > -1 && Cindex == 2)
            {
                string value = dgvDtls.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].FormattedValue.ToString();
                CardNo = value.ToString();

                try
                {
                    DataTable dt = new DataTable(); GlobalClass bll = new GlobalClass();
                    dt = bll.GetCommonInformation(3, CardNo);
                    MessageBox.Show(dt.Rows[0]["Token"].ToString());
                }
                catch { }
            }
        }
    }
}
