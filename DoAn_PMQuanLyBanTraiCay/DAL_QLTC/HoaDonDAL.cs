using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class HoaDonDAL
    {
        public List<HoaDonDTO> GetAllHoaDon()
        {
            List<HoaDonDTO> result = new List<HoaDonDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT MaHD, NgayLap, MaNV, MaKH, TongTien FROM HoaDon"; // Bao gồm MaKH
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new HoaDonDTO
                    {
                        MaHD = (int)reader["MaHD"],
                        NgayLap = (DateTime)reader["NgayLap"],
                        MaNV = reader["MaNV"] == DBNull.Value ? null : (int?)reader["MaNV"],
                        MaKH = reader["MaKH"] == DBNull.Value ? null : (int?)reader["MaKH"], // Bao gồm MaKH
                        TongTien = (decimal)reader["TongTien"]
                    });
                }
            }
            return result;
        }
        public int InsertAndGetID(HoaDonDTO hd)
        {
            int maHD = 0;
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = @"INSERT INTO HoaDon (NgayLap, MaNV, MaKH, TongTien) 
                                 OUTPUT INSERTED.MaHD
                                 VALUES (@NgayLap, @MaNV, @MaKH, @TongTien)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayLap", hd.NgayLap);
                cmd.Parameters.AddWithValue("@MaNV", (object)hd.MaNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKH", (object)hd.MaKH ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TongTien", hd.TongTien);
                conn.Open();
                maHD = (int)cmd.ExecuteScalar();
            }
            return maHD;
        }

        // Tìm kiếm hóa đơn theo từ khóa
        public List<HoaDonDTO> SearchHoaDon(string keyword)
        {
            List<HoaDonDTO> result = new List<HoaDonDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM HoaDon WHERE MaHD LIKE @keyword OR MaKH LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new HoaDonDTO
                    {
                        MaHD = (int)reader["MaHD"],
                        NgayLap = (DateTime)reader["NgayLap"],
                        MaNV = reader["MaNV"] == DBNull.Value ? null : (int?)reader["MaNV"],
                        MaKH = reader["MaKH"] == DBNull.Value ? null : (int?)reader["MaKH"],
                        TongTien = (decimal)reader["TongTien"]
                    });
                }
            }
            return result;
        }

        //  Xóa hóa đơn
        public bool DeleteHoaDon(int maHD)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction(); // Bắt đầu transaction

                try
                {
                    // Xóa dữ liệu trong bảng CT_HoaDon trước
                    string deleteChiTietQuery = "DELETE FROM CT_HoaDon WHERE MaHD = @maHD";
                    SqlCommand deleteChiTietCmd = new SqlCommand(deleteChiTietQuery, conn, transaction);
                    deleteChiTietCmd.Parameters.AddWithValue("@maHD", maHD);
                    deleteChiTietCmd.ExecuteNonQuery();

                    // Xóa dữ liệu trong bảng HoaDon
                    string deleteHoaDonQuery = "DELETE FROM HoaDon WHERE MaHD = @maHD";
                    SqlCommand deleteHoaDonCmd = new SqlCommand(deleteHoaDonQuery, conn, transaction);
                    deleteHoaDonCmd.Parameters.AddWithValue("@maHD", maHD);
                    deleteHoaDonCmd.ExecuteNonQuery();

                    transaction.Commit(); // Commit transaction nếu không có lỗi
                    return true;
                }
                catch
                {
                    transaction.Rollback(); // Rollback transaction nếu có lỗi
                    return false;
                }
            }
        }

        public bool UpdateHoaDon(HoaDonDTO hoaDon)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"UPDATE HoaDon 
                         SET NgayLap = @NgayLap, TongTien = @TongTien
                         WHERE MaHD = @MaHD";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", hoaDon.MaHD);
                cmd.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
                cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
