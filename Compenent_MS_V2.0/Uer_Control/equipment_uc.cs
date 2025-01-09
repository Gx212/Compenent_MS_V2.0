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
    public partial class equipment_uc : UserControl
    {
        public equipment_uc()
        {
            InitializeComponent();
            table_read();

        }

        public void table_read()//表格读取数据库内容，刷新表格数据方法
        {
            dataGridView1.Rows.Clear();
            Dao dao = new Dao();
            string sql = "select * from equipment_data";
            IDataReader data = dao.read(sql);
            int row_num = 0;

            while (data.Read())
            {
                dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString());
                if (dataGridView1.Rows[row_num].Cells[5].Value.ToString() == "未使用")
                {
                    dataGridView1.Rows[row_num].Cells[5].Style.BackColor = Color.Green;//刷新颜色
                }
                else
                {
                    dataGridView1.Rows[row_num].Cells[5].Style.BackColor = Color.Red;
                }
                row_num++;
            }
            data.Close();
            dao.DaoClose();
        }

        private void button1_Click(object sender, EventArgs e)//借用设备按钮
        {
            
            string lend_sql = $"UPDATE equipment_data SET 状态 = '已借用' WHERE 仪器名称 = '{label_u1.Text}'";
            Dao dao = new Dao();
            if (dao.Execute(lend_sql) > 0)
            {
                table_read();
                MessageBox.Show("借用成功");
                //dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Style.BackColor = Color.Red;
            }
            else {
                MessageBox.Show("借用失败");
            }
           

        }

        private void button2_Click(object sender, EventArgs e)//归还元设备按钮
        {
            string return_sql = $"UPDATE equipment_data SET 状态 = '未使用' WHERE 仪器名称 = '{label_u1.Text}'";
            Dao dao = new Dao();
            if (dao.Execute(return_sql) > 0)
            {
                MessageBox.Show("归还成功");
                table_read();
            }
            else {
                MessageBox.Show("归还失败");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)//添加设备按钮
        {
            import_equipment im_equipment = new import_equipment();
            im_equipment.ShowDialog();
            table_read();//返回后刷新

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//点击选中行
        {
            label_u1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label_u2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label_u3.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            label_u4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label_u5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        public Dictionary<string, int> GetEquipmentDate()
        {
            Dictionary<string,int> equipment_date = new Dictionary<string,int>();
            equipment_date.Add("已借用",0);
            equipment_date.Add("未借用", 0);


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[5].Value != null)
                {
                    if (row.Cells[5].Value.ToString() == "已借用")
                    {
                        equipment_date["已借用"] += 1;
                    }
                    else
                    {
                        equipment_date["未借用"] += 1;
                    }
                }
            }
            return equipment_date;
        }
    }


}
