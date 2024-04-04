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
    public partial class journal_uc : UserControl
    {
        public static journal_uc journal;
        public journal_uc()
        {
            InitializeComponent();
            journal = this;
        }

        public void readLog(string log)
        {
            string Time = Convert.ToString(DateTime.Now);
            journal.richTextBox1.AppendText(Time + " " + log + "\n");//注意使用实例化后的journal进行操作

        }
    }
}
