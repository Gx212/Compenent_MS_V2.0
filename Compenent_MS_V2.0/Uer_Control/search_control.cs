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
    public partial class search_control : UserControl
    {

        public search_control()
        {
            InitializeComponent();
            comboBox1.Text = comboBox1.Items[0].ToString();
        }

        private void search_control_Load(object sender, EventArgs e)
        {
            table_read();
            dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);//鼠标点击事件订阅

            label12.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label14.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label15.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label16.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            label17.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            label18.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();//默认显示第一行数据
        }

        private void button4_Click(object sender, EventArgs e)
        {
            import_Form im_form = new import_Form();
            im_form.ShowDialog();

        }

        public void table_read()//表格读取数据库内容，刷新表格数据方法
        {
            dataGridView1.Rows.Clear();
            Dao dao = new Dao();
            string sql = "select * from component_data";
            IDataReader data = dao.read(sql);

            while (data.Read())
            {
                dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString(), data[6].ToString());
            }
            data.Close();
            dao.DaoClose();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//点击单元格显示内容
        {
            label12.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label14.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label15.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label16.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            label17.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            label18.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();

            //List<ListViewItem> items  = new List<ListViewItem>(GetSelectedDataGridViewRows());

            //label19.Text = items.Count.ToString();//无法实现鼠标滑动触发事件

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)//鼠标点击滑动事件处理函数
        {
            DataGridViewSelectedRowCollection selectedRows = dataGridView1.SelectedRows;
            label19.Text = selectedRows.Count.ToString() + " 个";
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "（全部）")//实现全局搜索
            {
                comboBox1.Text = comboBox1.Items[0].ToString();//点击搜索后刷新combox1分类框显示

                dataGridView1.Rows.Clear();
                Dao dao = new Dao();
                string sql = $"select * from component_data where 名称 like '%{textBox1.Text}%'";
                IDataReader data = dao.read(sql);
                while (data.Read())
                {
                    dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString(), data[6].ToString());
                }
                data.Close();
                dao.DaoClose();
            }
            else
            {
                comboBox1.Text = comboBox1.Items[0].ToString();//点击搜索后刷新combox1分类框显示

                dataGridView1.Rows.Clear();
                Dao dao = new Dao();
                string sql = $"select * from component_data where 名称 like '%{textBox1.Text}%' and 类型 like '%{comboBox2.Text}%'";
                IDataReader data = dao.read(sql);
                while (data.Read())
                {
                    dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString(), data[6].ToString());
                }
                data.Close();
                dao.DaoClose();
            }

        }

        public void combox_query()//下拉框查询事件
        {
            dataGridView1.Rows.Clear();
            Dao dao = new Dao();
            string sql = $"select * from component_data where 类型 = '{comboBox1.Text}'";
            IDataReader data = dao.read(sql);

            while (data.Read())
            {
                dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString(), data[6].ToString());
            }
            data.Close();
            dao.DaoClose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "（全部）")
            {
                table_read();
            }
            else
            {
                combox_query();
            }
        }

        private void button2_Click(object sender, EventArgs e)//修改操作
        {
            String_arry.compenent_arry[0] = label12.Text;
            String_arry.compenent_arry[1] = label13.Text;
            String_arry.compenent_arry[2] = label14.Text;
            String_arry.compenent_arry[3] = label15.Text;
            String_arry.compenent_arry[4] = label16.Text;
            String_arry.compenent_arry[5] = label17.Text;
            String_arry.compenent_arry[6] = label18.Text;//将选中的元器件数据存入一个静态数组中，方便在其他页面进行访问操作

            Modify_Form mod_form = new Modify_Form();
            mod_form.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)//删除按钮
        {
            DialogResult dr = MessageBox.Show($"确定从数据库中移除{label13.Text}吗？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                Dao dao = new Dao();
                string sql = $"delete from component_data where 名称 = '{label13.Text}'";
                if (dao.Execute(sql) > 0)
                {
                    MessageBox.Show($"成功删除{label13.Text}");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)//导出界面
        {

            export_Form ex_form = new export_Form();
            ex_form.SetSelectedDataGridViewData(GetSelectedDataGridViewRows());//添加选中的数据
            ex_form.ShowDialog();

        }

        public List<ListViewItem> GetSelectedDataGridViewRows()//创建一个获取数据的方法
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();
            int count = 1;

            foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
            {
                ListViewItem item = new ListViewItem($"No.{count}");//第一列
                for (int i = 0; i < 4; i++)
                {
                    item.SubItems.Add(dgvRow.Cells[i].Value.ToString());
                }
                item.SubItems.Add(dgvRow.Cells[6].Value.ToString());
                selectedItems.Add(item);
                count++;
            }

            return selectedItems;
        }

        public Dictionary<string, int> GetDataGridViewData()//向count_uc中添加字典数据
        {
            Dictionary<string, int> dataDictionary = new Dictionary<string, int>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[6].Value != null)
                {      
                    string key = row.Cells[0].Value.ToString();
                    int value = Convert.ToInt32(row.Cells[6].Value);

                    if (dataDictionary.ContainsKey(key))//如果存在相同类型的键值
                    {

                        dataDictionary[key] = value + dataDictionary[key];//将对应值添加
                    }
                    else
                    {
                        dataDictionary.Add(key, value);//避免添加相同的键值
                    }
                }
            }

            return dataDictionary;
        }
    }


    public static class String_arry//创建一个静态数组存储数据，不同页面访问
    {
        public static String[] compenent_arry = new String[7];
    }
}


   

