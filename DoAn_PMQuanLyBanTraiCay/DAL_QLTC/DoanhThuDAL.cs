using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class DoanhThuDAL
    {
        public List<DoanhThuDTO> GetDoanhThuTheoNgay()
        {
            List<DoanhThuDTO> list = new List<DoanhThuDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = @"
                SELECT hd.NgayLap, SUM(ct.ThanhTien) AS DoanhThu
                FROM HoaDon hd
                JOIN CT_HoaDon ct ON hd.MaHD = ct.MaHD
                GROUP BY hd.NgayLap
                ORDER BY hd.NgayLap DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DoanhThuDTO
                    {
                        Ngay = (DateTime)reader["NgayLap"],
                        DoanhThu = (decimal)reader["DoanhThu"]
                    });
                }
            }
            return list;
        }
    }

}
