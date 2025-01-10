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
    public partial class nuclide_uc : UserControl
    {
        public nuclide_uc()
        {
            InitializeComponent();
        }

        private void nuclide_uc_Load(object sender, EventArgs e)
        {
            table_read();
            label_u1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label_u2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label_u3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label_u4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label_u5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            label_u6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            label_u7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            label_u8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            label_u9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
        }
        public void table_read()//表格读取数据库内容，刷新表格数据方法
        {
            dataGridView1.Rows.Clear();
            Dao dao = new Dao();
            string sql = "select * from nuclide_data";
            IDataReader data = dao.read(sql);

            while (data.Read())
            {
                dataGridView1.Rows.Add(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString(), data[5].ToString(), data[6].ToString(), data[7].ToString(), data[8].ToString());//将核素信息加载在表格中
            }
            data.Close();
            dao.DaoClose();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//选中数据显示
        {
            label_u1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label_u2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label_u3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label_u4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label_u5.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            label_u6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            label_u7.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            label_u8.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            label_u9.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)//导入核素
        {
            nuclideImport_Form nucimport_form = new nuclideImport_Form();
            nucimport_form.ShowDialog();
            table_read();
        }
    }
}
