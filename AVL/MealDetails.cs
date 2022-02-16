using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVL
{
    public partial class MealDetails : Form
    {
        public MealDetails()
        {
            InitializeComponent();
            DataTable dt = new DataTable(); BLLClass bll = new BLLClass();
            dt = bll.GetTodaymealList();
            dgvDtls.DataSource = dt;
        }
    }
}
