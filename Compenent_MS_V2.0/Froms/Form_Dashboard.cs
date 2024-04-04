using Compenent_MS_V2._0.Uer_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compenent_MS_V2._0.Froms
{

    public partial class Form_Dashboard : Form
    {
        public Form_Dashboard()
        {
            InitializeComponent();
        }

        int PanelWith;
        bool isCollapsed;
        private void Form_Dashboard_Load(object sender, EventArgs e)
        {
            Time_Date.Start();//显示当前时间

            search_control sea_uc = new search_control();
            add_Control(sea_uc);//默认显示搜索页面

        }


        public void moveSidePanel(Control btn)//控制 选中方块 的移动
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }


        public void add_Control(Control c)//添加pane_uc1界面
        {
            c.Dock = DockStyle.Fill;
            panel_control.Controls.Clear();
            panel_control.Controls.Add(c);
        }


        private void Time_Date_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            label2.Text = dateTime.ToString("HH:mm:ss");
        }

        private void button2_Click(object sender, EventArgs e)//返回主界面
        {
            this.Dispose();
        }


        private void button4_Click(object sender, EventArgs e)//统计
        {
            moveSidePanel(button4);
            count_uc count_uc = new count_uc();//添加图表
            add_Control(count_uc);
        }

        private void button5_Click(object sender, EventArgs e)//日志
        {
            moveSidePanel(button5);
            journal_uc jo_uc = new journal_uc();
            add_Control(jo_uc);
        }

        private void button_search_Click(object sender, EventArgs e)//查询
        {
            moveSidePanel(button_search);
            search_control sea_uc = new search_control();
            add_Control(sea_uc);//默认显示搜索页面
        }

        private void button6_Click(object sender, EventArgs e)//刷新按钮
        {
            //search_control serch_cu = new search_control();
            //serch_cu.table_read();
            
            search_control search_uc = new search_control();
            add_Control(search_uc);
        }
    }
}
