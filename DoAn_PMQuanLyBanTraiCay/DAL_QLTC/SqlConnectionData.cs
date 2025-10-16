using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public static class SqlConnectionData
    {
        public static string ConnectionString = "Data Source=LAPTOP-U7PJAHA4\\SQL;Initial Catalog=QuanLyTraiCay;Integrated Security=True";
        public static SqlConnection Connect()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
