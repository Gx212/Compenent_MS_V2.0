using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Compenent_MS_V2._0.Froms
{
    public partial class Register_Form : Form
    {
        public Register_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//返回按钮
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)//注册按钮
        {
            Dao dao = new Dao();
            string sql_add_user = $"insert into user_data values ('{textBox3.Text}','{textBox1.Text}','{textBox2.Text}','{textBox4.Text}') ";

            if (dao.Execute(sql_add_user) > 0)
            {
                MessageBox.Show("添加新用户成功");
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
    }
}
