using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Compenent_MS_V2._0.Froms
{
    public partial class import_Form : Form
    {
        public import_Form()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)//返回按钮
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)//手动导入
        {
               //如何型号不一样，则循环查看数据库中是否存在增添的型号，如果有则在元数据基础上增加数量，否则新增添数据
            
                Dao dao = new Dao();
                string sql = "select * from component_data";
                IDataReader data = dao.read(sql);
                Boolean isExist = false;
                while (data.Read())
                {
                    if (data[1].ToString() == textBox2.Text)//存在该型号
                    {
                        DialogResult dialog = MessageBox.Show($"查询到数据库中存在型号：{textBox2.Text},是否增添该元器件数量？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dialog == DialogResult.OK)
                        {
                            int basic_data;
                            int add_data;
                            Int32.TryParse(data[6].ToString(), out basic_data);
                            Int32.TryParse(textBox7.Text, out add_data);//将string转换成int类型数据进行运算

                            string sql2 = $"update component_data set 数量 = '{(basic_data + add_data).ToString()}' where 名称 = '{textBox2.Text}'";

                            if (dao.Execute(sql2) > 0)
                            {
                                MessageBox.Show("增添成功");
                            }
                            dao.DaoClose();
                            isExist = true;
                            break;
                        }
                    }
                }

                if (isExist == false)//不存在
                {
                    Dao dao2 = new Dao();
                    DialogResult dialog2 = MessageBox.Show("未存在相同数据，是否增添为新元器件数据？", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dialog2 == DialogResult.OK)
                    {
                        string sql3 = $"insert into component_data values ('{comboBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','{textBox6.Text}','{textBox7.Text}')";
                        if (dao2.Execute(sql3) > 0)
                        {
                            MessageBox.Show("添加新元器件成功");
                        }

                    }
                }
            
        }

        private void button2_Click(object sender, EventArgs e)//浏览文件
        {
            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = fd.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)//excel表格导入数据
        {
            try
            {
                ExcelPackage package = new ExcelPackage(new FileInfo($"{textBox8.Text}"));
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];//选取第一个工作表

                Dao dao = new Dao();
                int exist_component = 0;
                int noexist_component = 0;


               for (int Row = 1; Row <= worksheet.Dimension.Rows; Row++)
               {
                    string sql = $"SELECT COUNT(*) FROM component_data where 名称 = '{worksheet.Cells[Row,2].Value}'";
                    int count = dao.ExecuteScalar(sql);

                    if (count > 0)//存在相同型号的元器件，数量累加
                    {
                        string sql_query = "select * from component_data";//注意
                        IDataReader data = dao.read(sql_query);
                        data.Read();
                        int basic_data;
                        int add_data;
                        Int32.TryParse(data[6].ToString(), out basic_data);
                        Int32.TryParse(worksheet.Cells[Row,7].Value.ToString(), out add_data);//将string转换成int类型数据进行运算
                        string sql_insert = $"update component_data set 数量 = '{(basic_data + add_data).ToString()}' where 名称 = '{worksheet.Cells[Row,2].Value}'";
                        dao.Execute(sql_insert);

                        exist_component++;

                    }
                    else//不存在该型号的元器件，执行插入
                    {
                        string sql_insert = $"insert into component_data values ('{worksheet.Cells[Row, 1].Value}','{worksheet.Cells[Row, 2].Value}','{worksheet.Cells[Row, 3].Value}','{worksheet.Cells[Row, 4].Value}','{worksheet.Cells[Row, 5].Value}','{worksheet.Cells[Row, 6].Value}',{worksheet.Cells[Row, 7].Value})";
                        dao.Execute(sql_insert);

                        noexist_component++;
                    }

               }

                dao.DaoClose();
                MessageBox.Show($"新添加{noexist_component}个元器件，{exist_component}个元器件数量增添");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Erro");
            }
        }
    }
}
