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

namespace Compenent_MS_V2._0
{
    public partial class Form1 : Form
    {
        public static string user_Name;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();//释放所有资源，关闭窗口
        }

        private void button1_Click(object sender, EventArgs e)//登录按钮
        {
            login();
        }

        private void button3_Click(object sender, EventArgs e)//注册账号按钮
        {

        }

        public void login()
        {
            Dao dao = new Dao();
            string sql = $"select * from user_data where user_id = '{textBox1.Text}' and user_pasw = '{textBox2.Text}'";//查询语句
            IDataReader user_data = dao.read(sql);
            if (user_data.Read())
            {
                user_Name = user_data["user_name"].ToString();
                using (Form_Dashboard fd = new Form_Dashboard())
                {
                    this.Hide();
                    fd.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//显示密码选项
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }
    }
}
