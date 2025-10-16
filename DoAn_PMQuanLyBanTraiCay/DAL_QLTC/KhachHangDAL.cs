using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class KhachHangDAL
    {
        public List<KhachHangDTO> GetAll()
        {
            List<KhachHangDTO> result = new List<KhachHangDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM KhachHang";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new KhachHangDTO
                    {
                        MaKH = (int)reader["MaKH"],
                        HoTen = reader["HoTen"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        LoaiKhachHang = reader["LoaiKhachHang"].ToString()
                    });
                }
            }
            return result;
        }
         
        public int ThemVaLayMaKH(KhachHangDTO kh)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"INSERT INTO KhachHang (HoTen, SDT, Email, DiaChi, LoaiKhachHang)
                                 OUTPUT INSERTED.MaKH
                                 VALUES (@HoTen, @SDT, @Email, @DiaChi, @LoaiKhachHang)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                cmd.Parameters.AddWithValue("@SDT", kh.SDT);
                cmd.Parameters.AddWithValue("@Email", (object)kh.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)kh.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiKhachHang", kh.LoaiKhachHang);

                try
                {
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
                catch
                {
                    return -1;
                }
            }
        }
        public bool ThemKhachHang(KhachHangDTO kh)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = @"INSERT INTO KhachHang (HoTen) VALUES (@HoTen)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HoTen", kh.HoTen);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool AddKhachHang(KhachHangDTO khachHang)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"INSERT INTO KhachHang (HoTen, SDT, Email, DiaChi, LoaiKhachHang) 
                         VALUES (@HoTen, @SDT, @Email, @DiaChi, @LoaiKhachHang)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen);
                cmd.Parameters.AddWithValue("@SDT", khachHang.SDT);
                cmd.Parameters.AddWithValue("@Email", (object)khachHang.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)khachHang.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiKhachHang", khachHang.LoaiKhachHang);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateKhachHang(KhachHangDTO khachHang)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"UPDATE KhachHang 
                                 SET HoTen = @HoTen, SDT = @SDT, Email = @Email, 
                                     DiaChi = @DiaChi, LoaiKhachHang = @LoaiKhachHang 
                                 WHERE MaKH = @MaKH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKH", khachHang.MaKH);
                cmd.Parameters.AddWithValue("@HoTen", khachHang.HoTen);
                cmd.Parameters.AddWithValue("@SDT", khachHang.SDT);
                cmd.Parameters.AddWithValue("@Email", (object)khachHang.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)khachHang.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LoaiKhachHang", khachHang.LoaiKhachHang);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteKhachHang(int maKH)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Xóa dữ liệu tham chiếu trong bảng TaiKhoan (hoặc các bảng khác)
                    string deleteTaiKhoanQuery = "DELETE FROM TaiKhoan WHERE MaKH = @MaKH";
                    SqlCommand cmdDeleteTaiKhoan = new SqlCommand(deleteTaiKhoanQuery, conn, transaction);
                    cmdDeleteTaiKhoan.Parameters.AddWithValue("@MaKH", maKH);
                    cmdDeleteTaiKhoan.ExecuteNonQuery();

                    // Xóa khách hàng trong bảng KhachHang
                    string deleteKhachHangQuery = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                    SqlCommand cmdDeleteKhachHang = new SqlCommand(deleteKhachHangQuery, conn, transaction);
                    cmdDeleteKhachHang.Parameters.AddWithValue("@MaKH", maKH);
                    cmdDeleteKhachHang.ExecuteNonQuery();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }

        }

        public List<KhachHangDTO> SearchKhachHang(string keyword)
        {
            List<KhachHangDTO> result = new List<KhachHangDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"SELECT MaKH, HoTen, SDT, Email, DiaChi, LoaiKhachHang, NgayTao 
                                 FROM KhachHang
                                 WHERE HoTen LIKE @Keyword OR SDT LIKE @Keyword OR DiaChi LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new KhachHangDTO
                    {
                        MaKH = (int)reader["MaKH"],
                        HoTen = reader["HoTen"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"]?.ToString(),
                        DiaChi = reader["DiaChi"]?.ToString(),
                        LoaiKhachHang = reader["LoaiKhachHang"].ToString(),
                        NgayTao = (DateTime)reader["NgayTao"]
                    });
                }
            }
            return result;
        }
    }
}
