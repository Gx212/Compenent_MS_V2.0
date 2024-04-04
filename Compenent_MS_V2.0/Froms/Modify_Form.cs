using Compenent_MS_V2._0.Uer_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compenent_MS_V2._0.Froms
{
    public partial class Modify_Form : Form
    {
        public Modify_Form()
        {
            InitializeComponent();
        }

        private void Modify_Form_Load(object sender, EventArgs e)
        {
            data_read();
            textBox2.Enabled = false;//关闭型号编辑
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //search_control sea_uc = new search_control();
            //sea_uc.table_read();只刷新了searach界面，没有刷新仪表界

            this.Dispose();
            
        }

        private void button1_Click(object sender, EventArgs e)//修改按钮
        {
            DialogResult dr = MessageBox.Show($"确认修改{textBox2.Text}吗？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)//确定修改
            {
                if (textBox2.Text == String_arry.compenent_arry[1])//型号未修改，执行更新操作
                {
                    Dao dao = new Dao();
                    String sql = $"update  component_data set 类型 = '{comboBox1.Text}',参数='{textBox3.Text}',封装='{textBox4.Text}',描述='{textBox5.Text}',链接='{textBox6.Text}',数量='{textBox7.Text}' where 名称 = '{textBox2.Text}'";

                    if (dao.Execute(sql) > 0)
                    {
                        MessageBox.Show("修改成功");
                    }
                    else
                    {
                        MessageBox.Show("修改失败");
                    }
                    dao.DaoClose();

                }
               
                
            }
        }
        public void data_read()//读取信息显示
        {
            comboBox1.Text = String_arry.compenent_arry[0];
            textBox2.Text = String_arry.compenent_arry[1];
            textBox3.Text = String_arry.compenent_arry[2];
            textBox4.Text = String_arry.compenent_arry[3];
            textBox5.Text = String_arry.compenent_arry[4];
            textBox6.Text = String_arry.compenent_arry[5];
            textBox7.Text = String_arry.compenent_arry[6];
        }
    }
}
