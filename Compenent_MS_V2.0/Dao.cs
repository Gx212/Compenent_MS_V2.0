using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Compenent_MS_V2._0
{
    internal class Dao
    {
        public MySqlConnection Connection()//数据库连接
        {
            String Connection = "data source=localhost;database=component_MS;user id=root;password=123456;pooling=true;charset=utf8;";
            MySqlConnection mscn = new MySqlConnection(Connection);
            mscn.Open();
            return mscn;
        }

        public MySqlCommand Command(string sql) //sql为数据库命令
        {
            MySqlCommand msc = new MySqlCommand(sql, Connection());
            return msc;
        }

        public int Execute(string sql)
        {
            return Command(sql).ExecuteNonQuery();//执行传入的 SQL 命令，例如 INSERT、UPDATE 或 DELETE，然后返回受影响的行数，这样您可以知道操作对数据库中的行数产生了什么影响。
        }

        public int ExecuteScalar(string sql)
        {
            return Convert.ToInt32(Command(sql).ExecuteScalar());//执行SELECT COUNT(*)查询功能，返回count值
        }
        public MySqlDataReader read(string sql)
        {
            return Command(sql).ExecuteReader();//对象可以用于逐行读取查询结果集中的数据。
        }

        public void DaoClose()//关闭数据库连接
        {
            Connection().Close();
        }
    }
}
