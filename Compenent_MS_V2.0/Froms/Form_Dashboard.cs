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

    //这是推送到github的第二次测试
    public partial class Form_Dashboard : Form
    {
        public static Form_Dashboard f_dashboard;//定义静态变量

        public Form_Dashboard()
        {
            InitializeComponent();
            f_dashboard = this;//静态变量实例化
           
        }

    
        private void Form_Dashboard_Load(object sender, EventArgs e)
        {
            Time_Date.Start();//显示当前时间
            user_name.Text = Form1.user_Name;

            //search_control sea_uc = new search_control();
            //add_Control(sea_uc);//默认显示搜索页面
            Dashboard_uc dashboard_uc = new Dashboard_uc();
            add_Control(dashboard_uc);//默认显示导航页面

        }


        public void moveSidePanel(Control btn)//控制 选中方块 的移动
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }


        public void add_Control(Control c)//添加pane_uc界面
        {
            c.Dock = DockStyle.Fill;
            panel_control.Controls.Clear();
            panel_control.Controls.Add(c);
        }


        private void Time_Date_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            label2.Text = dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private void button2_Click(object sender, EventArgs e)//返回主界面
        {
            this.Dispose();
        }


        private void button4_Click(object sender, EventArgs e)//统计页面
        {
            moveSidePanel(button4);
            count_uc count_uc = new count_uc();//添加图表
            add_Control(count_uc);

        }

        private void button5_Click(object sender, EventArgs e)//日志页面
        {
            moveSidePanel(button5);
            journal_uc jo_uc = new journal_uc();
            add_Control(jo_uc);
        }

        private void button_dashboard_Click(object sender, EventArgs e)//导航页面
        {
            moveSidePanel(button_dashboard);
            //search_control sea_uc = new search_control();
            //add_Control(sea_uc);//默认显示搜索页面

            Dashboard_uc dashboard = new Dashboard_uc();
            add_Control(dashboard);//显示导航界面
        }

        private void button6_Click(object sender, EventArgs e)//刷新按钮
        {
            moveSidePanel(button_dashboard);
            Dashboard_uc dashboard_uc = new Dashboard_uc();
            add_Control(dashboard_uc);
        }

        public void turn_panel(Control c)//构建其他页面调用方法
        {
            c.Dock = DockStyle.Fill;
            f_dashboard.panel_control.Controls.Clear();
            f_dashboard.panel_control.Controls.Add(c);
   
        }


    }
}
