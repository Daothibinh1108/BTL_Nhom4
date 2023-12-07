using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Quan_Ly_Hieu_Thuoc
{
    class connect
    {
        SqlConnection sqlcon;
        SqlDataAdapter adapter;
        DataSet ds;

        public connect()
        {
            string connect = @"Data Source=DESKTOP-6E3QV61\SQLEXPRESS;Initial Catalog=HieuThuoc04_N05;Integrated Security=True";
            sqlcon = new SqlConnection(connect);
        }

        public DataTable Execute(string sqlquery)
        {
            adapter = new SqlDataAdapter(sqlquery, sqlcon);
            ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public void ExcuteNonQuery(string sqlquery)
        {
            SqlCommand cmd = new SqlCommand(sqlquery, sqlcon);
            sqlcon.Open();
            cmd.ExecuteNonQuery();
            sqlcon.Close();
        }
    }
}
