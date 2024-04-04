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

namespace Compenent_MS_V2._0.Uer_Control
{
    public partial class count_uc : UserControl
    {
        Dictionary<string, int> componentsCount = new Dictionary<string, int>();//创建数据字典
        public count_uc()
        {
            InitializeComponent();
        }

        private void count_uc_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.Title = "元器件类型";
            chart1.ChartAreas[0].AxisY.Title = "数量";

            AddDataFromForm1();
        


            chart1.Series["元器件数量统计"].ChartType = SeriesChartType.Column;
            

            foreach (var item in componentsCount)//添加对应数据
            {
                chart1.Series["元器件数量统计"].Points.AddXY(item.Key, item.Value);
            }


        }


        private void AddDataFromForm1()
        {
            search_control sear_cu = new search_control();

            Dictionary<string, int> dataFromForm1 = sear_cu.GetDataGridViewData();

            foreach (var pair in dataFromForm1)
            {
                componentsCount.Add(pair.Key, pair.Value);
            }
        }
    }
}
