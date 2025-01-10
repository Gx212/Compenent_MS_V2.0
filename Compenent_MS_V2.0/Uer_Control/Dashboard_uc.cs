using Compenent_MS_V2._0.Froms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compenent_MS_V2._0.Uer_Control
{
    public partial class Dashboard_uc : UserControl
    {
        public Dashboard_uc()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)//跳转元器件管理页面
        {
           this.Dispose();
            search_control search_control = new search_control();
            Form_Dashboard.f_dashboard.turn_panel(search_control);

        }

        private void button1_Click(object sender, EventArgs e)//跳转实验器材管理页面
        {
            this.Dispose();
            equipment_uc equipment_uc = new equipment_uc();
            Form_Dashboard.f_dashboard.turn_panel(equipment_uc);
        }

        private void button2_Click(object sender, EventArgs e)//跳转核素信息页面
        {
            this.Dispose();
            nuclide_uc nuc_uc = new nuclide_uc();
            Form_Dashboard.f_dashboard.turn_panel(nuc_uc);

        }
    }
}
