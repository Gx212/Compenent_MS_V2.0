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
    public partial class import_equipment : Form
    {
        public import_equipment()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//导入按钮
        {
            string import_sql = $"insert into equipment_data values ('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox5.Text}','{textBox4.Text}','{comboBox1.Text}')";
            Dao dao = new Dao();
            if (dao.Execute(import_sql) > 0)
            {
                MessageBox.Show("导入成功");
            }
            else 
            {
                MessageBox.Show("导入失败");
            }
        }

        private void button4_Click(object sender, EventArgs e)//返回按钮
        {
            this.Dispose();
        }
    }
}
