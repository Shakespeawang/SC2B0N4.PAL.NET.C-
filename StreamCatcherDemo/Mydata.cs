using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace StreamCatcherDemo
{
    class Mydata
    {
         static uint number = getcameranum();
        //public Mydata(){
        public static string conn_str = @"Data Source=VPGKN7VP7JTJ06Z;Initial Catalog=a2;User ID=sa;Pwd=123456";
        //}
        // public static uint cameranumber = getcameranum();
        //返回数据库中摄像头个数
        public static uint getcameranum()
        {

            string sqlstr = "select value from mydata where properties = 'cameranum' ";
            string camera_num = getConSql(sqlstr);
            uint num = uint.Parse(camera_num);

            return num;

        }
        public static string getConSql(string sqlstr)
        {

            string constr = conn_str;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            string result = cmd.ExecuteScalar().ToString();
            cmd.Dispose();
            conn.Close();
            return result;

        }

       
    }
}
