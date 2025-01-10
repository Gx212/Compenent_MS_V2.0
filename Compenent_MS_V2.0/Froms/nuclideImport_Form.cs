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

namespace Compenent_MS_V2._0.Froms
{
    public partial class nuclideImport_Form : Form
    {
        public nuclideImport_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//退出
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)//浏览文件
        {

            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Path_text.Text = fd.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)//导入数据(复用导入元器件代码)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;//添加许可证
            try
            {
                ExcelPackage package = new ExcelPackage(new FileInfo($"{Path_text.Text}"));
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];//选取第一个工作表

                Dao dao = new Dao();
                int exist_nuclide = 0;
                int noexist_nuclide = 0;


                for (int Row = 1; Row <= worksheet.Dimension.Rows; Row++)
                {
                    string sql = $"SELECT COUNT(*) FROM nuclide_data where 序号 = '{worksheet.Cells[Row, 1].Value}'";
                    int count = dao.ExecuteScalar(sql);

                    if (count > 0)//存在相同核素
                    {
                        exist_nuclide++;//不进行操作,记录数量
                    }
                    else//不存在该核素，执行插入
                    {
                        string sql_insert = $"insert into nuclide_data values ('{worksheet.Cells[Row, 1].Value}','{worksheet.Cells[Row, 2].Value}','{worksheet.Cells[Row, 3].Value}','{worksheet.Cells[Row, 4].Value}','{worksheet.Cells[Row, 5].Value}','{worksheet.Cells[Row, 6].Value}','{worksheet.Cells[Row, 7].Value}','{worksheet.Cells[Row, 8].Value}','{worksheet.Cells[Row, 9].Value}','{worksheet.Cells[Row, 9].Value}')";
                        dao.Execute(sql_insert);

                        noexist_nuclide++;
                    }

                }

                dao.DaoClose();
                MessageBox.Show($"新添加{noexist_nuclide}种核素，{exist_nuclide}个核素已存在");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro");
            }
        }
    }
}
