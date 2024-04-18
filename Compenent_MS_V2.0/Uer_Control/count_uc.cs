using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Compenent_MS_V2._0.Uer_Control
{
    public partial class count_uc : UserControl
    {
        Dictionary<string, int> componentsCount = new Dictionary<string, int>();//创建数据字典
        Dictionary<string, int> equipment_Count = new Dictionary<string, int>();//创建器材字典
        public count_uc()
        {
            InitializeComponent();
        }

        private void count_uc_Load(object sender, EventArgs e)
        {
            load_chart();
        }

        public void load_chart()
        {
            //柱状图绘制（元器件数据）
            AddDataFromForm1();
            chart1.ChartAreas[0].AxisX.Title = "元器件类型";
            chart1.ChartAreas[0].AxisY.Title = "数量";

            chart1.Series["元器件数量统计"].ChartType = SeriesChartType.Column;


            foreach (var item in componentsCount)//添加对应数据
            {
                chart1.Series["元器件数量统计"].Points.AddXY(item.Key, item.Value);
            }


            //饼状图绘制（实验室器材数据）
            Title title = new Title();
            title.Text = "实验器材使用情况";
            chart2.Titles.Add(title);
            List<string> X_data = new List<string>();//创建x轴数据列表
            List<int> Y_data = new List<int>();//创建Y轴数据列表
            foreach (var item in equipment_Count)
            {
                X_data.Add(item.Key);
                Y_data.Add(item.Value);
            }


            chart2.Series[0].Points.DataBindXY(X_data, Y_data);
            chart2.Invalidate();
        }


        private void AddDataFromForm1()//获取datagridview的数据
        {
            search_control sear_cu = new search_control();

            Dictionary<string, int> dataFromForm1 = sear_cu.GetDataGridViewData();

            foreach (var pair in dataFromForm1)
            {
                componentsCount.Add(pair.Key, pair.Value);
            }

            equipment_uc equ_uc = new equipment_uc();
            Dictionary<string, int> dataFromForm2 = equ_uc.GetEquipmentDate();

            foreach (var pair in dataFromForm2)
            {
                equipment_Count.Add(pair.Key, pair.Value);
            }

        }



    }
}
