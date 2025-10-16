using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class NhanVienDAL
    {
        public List<NhanVienDTO> GetAll()
        {
            List<NhanVienDTO> result = new List<NhanVienDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM NhanVien";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new NhanVienDTO
                    {
                        MaNV = (int)reader["MaNV"],
                        TenNV = reader["TenNV"].ToString(),
                        NgaySinh = (DateTime)reader["NgaySinh"],
                        GioiTinh = reader["GioiTinh"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString(),
                        DiaChi = reader["DiaChi"].ToString()
                    });
                }
            }
            return result;
        }
        // Thêm nhân viên mới và trả về mã nhân viên
        public int ThemVaLayMaNV(NhanVienDTO nv)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"INSERT INTO NhanVien (TenNV, NgaySinh, GioiTinh, SDT, Email, DiaChi)
                         OUTPUT INSERTED.MaNV
                         VALUES (@TenNV, @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@NgaySinh", (object)nv.NgaySinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", (object)nv.GioiTinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", nv.SDT);
                cmd.Parameters.AddWithValue("@Email", (object)nv.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)nv.DiaChi ?? DBNull.Value);

                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1; // Trả về MaNV tự sinh
            }
        }
       
         

        // Sửa thông tin nhân viên
        public bool SuaNhanVien(NhanVienDTO nv)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"UPDATE NhanVien 
                                 SET TenNV = @TenNV, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                                     SDT = @SDT, Email = @Email, DiaChi = @DiaChi
                                 WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@TenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@NgaySinh", (object)nv.NgaySinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiTinh", (object)nv.GioiTinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SDT", nv.SDT);
                cmd.Parameters.AddWithValue("@Email", (object)nv.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DiaChi", (object)nv.DiaChi ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Xóa nhân viên
        public bool XoaNhanVien(int maNV)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Tìm kiếm nhân viên theo từ khóa
        public List<NhanVienDTO> TimKiemNhanVien(string keyword)
        {
            List<NhanVienDTO> result = new List<NhanVienDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"SELECT * FROM NhanVien 
                                 WHERE TenNV LIKE @keyword OR SDT LIKE @keyword OR Email LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new NhanVienDTO
                    {
                        MaNV = (int)reader["MaNV"],
                        TenNV = reader["TenNV"].ToString(),
                        NgaySinh = reader["NgaySinh"] == DBNull.Value ? null : (DateTime?)reader["NgaySinh"],
                        GioiTinh = reader["GioiTinh"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                        DiaChi = reader["DiaChi"] == DBNull.Value ? null : reader["DiaChi"].ToString(),
                        NgayTao = (DateTime?)reader["NgayTao"]
                    });
                }
            }
            return result;
        }
    }
}