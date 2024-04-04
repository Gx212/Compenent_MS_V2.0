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
    public partial class export_Form : Form
    {
        public export_Form()
        {
            InitializeComponent();
            label5.Text = "";
            label6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)//返回按钮
        {
            this.Dispose();
        }

        public void SetSelectedDataGridViewData(List<ListViewItem> items)//接收数据
        {
            if (items != null && items.Count > 0)
            {
                foreach (ListViewItem item in items)
                {
                    listView1.Items.Add(item);  
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)//选定某一项
        {
            if (e.IsSelected)//确认新点击事件
            {
                ListViewItem selectedListViewItem = e.Item;
                label5.Text = selectedListViewItem.SubItems[2].Text;
                label6.Text = selectedListViewItem.SubItems[5].Text;    
            }

        }

        private void button3_Click(object sender, EventArgs e)//点击修改导出数量
        {
            if (listView1.SelectedItems.Count > 0)//在调用索引之前检查是否选中
            {
                int selectedIndex = listView1.SelectedIndices[0];
                if (listView1.Items[selectedIndex].SubItems.Count <= 6)//实现重复修改，注意每个item中的subitem的索引是从0开始
                {
                    listView1.Items[selectedIndex].SubItems.Add(textBox1.Text);
                }
                else
                {
                    listView1.Items[selectedIndex].SubItems[6].Text = textBox1.Text;
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)//导出按钮
        {
            Boolean is_enough = true;//判断元器件是否足够
            Dao dao = new Dao();
            //string sql_delete = $"delete from component_data where 名称 = ''";
            //string sql_updata = $"updata component_data set 数量 = '' where 名称 = ''";

            //$"update  component_data set 类型 = '{comboBox1.Text}',参数='{textBox3.Text}',封装='{textBox4.Text}',描述='{textBox5.Text}',链接='{textBox6.Text}',数量='{textBox7.Text}' where 名称 = '{textBox2.Text}'";//
            //string sql = $"delete from component_data where 名称 = '{label13.Text}'";//

            foreach (ListViewItem item in listView1.Items)
            {
                if (Convert.ToInt32(item.SubItems[5].Text) < Convert.ToInt32(item.SubItems[6].Text))
                {
                    is_enough = false;
                    break;
                }
            }

            try
            {
                if (is_enough == false)
                {
                    MessageBox.Show("元器件库存不够，请检查！");
                }
                else
                {
                    foreach (ListViewItem item in listView1.Items)//导出程序
                    {
                        string num = Convert.ToString(Convert.ToInt32(item.SubItems[5].Text) - Convert.ToInt32(item.SubItems[6].Text));
                        if (Convert.ToInt32(num) == 0)
                        {
                            string sql_delete = $"delete from component_data where 名称 = '{item.SubItems[2].Text}'";
                            dao.Execute(sql_delete);//直接删除
                        }
                        else
                        {
                            string sql_updata = $"update component_data set 数量 = '{num}' where 名称 = '{item.SubItems[2].Text}'";

                            dao.Execute(sql_updata);//执行修改
                        }
                    }
                    MessageBox.Show("导出成功");
                }

            
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生异常：" + ex.Message);
            }

        }
    }
}
