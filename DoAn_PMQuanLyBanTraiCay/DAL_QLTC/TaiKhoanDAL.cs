using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class TaiKhoanDAL
    {

        public static string LayMatKhau(string tenDN, string email)
        {
            string mk = null;
            string query = "SELECT MK FROM TaiKhoan WHERE TenDN = @TenDN AND Email = @Email";

            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDN", tenDN);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    mk = result.ToString();
                }
            }

            return mk;
        }
        public TaiKhoanDTO DangNhap(string loginName, string password)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"
                SELECT * FROM TaiKhoan 
                WHERE (TenDN = @LoginName OR Email = @LoginName OR SDT = @LoginName)
                    AND MK = @Password
                    AND TrangThai = 'HoatDong'
                ";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@LoginName", loginName);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new TaiKhoanDTO
                    {
                        TenDN = reader["TenDN"].ToString(),
                        MK = reader["MK"].ToString(),
                        LoaiTK = reader["LoaiTK"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                        Email = reader["Email"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        MaNV = reader["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaNV"]),
                        MaKH = reader["MaKH"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaKH"])
                    };
                }
                return null;
            }
        }


        public bool TaoTaiKhoan(TaiKhoanDTO tk)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO TaiKhoan (TenDN, MK, LoaiTK, TrangThai, Email, SDT, MaNV, MaKH) VALUES (@TenDN, @MK, @LoaiTK, @TrangThai, @Email, @SDT, @MaNV, @MaKH)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDN", tk.TenDN);
                cmd.Parameters.AddWithValue("@MK", tk.MK);
                cmd.Parameters.AddWithValue("@LoaiTK", tk.LoaiTK);
                cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai); 
                cmd.Parameters.AddWithValue("@Email", tk.Email);
                cmd.Parameters.AddWithValue("@SDT", tk.SDT);
                cmd.Parameters.AddWithValue("@MaNV", (object)tk.MaNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKH", (object)tk.MaKH ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public TaiKhoanDTO GetByTenDangNhap(string tenDN)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM TaiKhoan WHERE TenDN = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDN);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TaiKhoanDTO
                        {
                            TenDN = reader["TenDN"].ToString(),
                            MK = reader["MK"].ToString(),
                            TrangThai = reader["TrangThai"].ToString(),
                            LoaiTK = reader["LoaiTK"].ToString(),
                            Email = reader["Email"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            MaNV = reader["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaNV"]),
                            MaKH = reader["MaKH"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaKH"])
                        };
                    }
                }
            }
            return null;
        }

        public bool DoiMatKhau(string tenDN, string mkMoi)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string sql = "UPDATE TaiKhoan SET MK = @MK WHERE TenDN = @TenDN";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MK", mkMoi);
                cmd.Parameters.AddWithValue("@TenDN", tenDN.Trim());
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool QuenMatKhau(string email, string mkMoi)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "UPDATE TaiKhoan SET MK = @MKMoi WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MKMoi", mkMoi);
                cmd.Parameters.AddWithValue("@Email", email);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public string LayLoaiTaiKhoan(string tenDangNhap)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT LoaiTK FROM TaiKhoan WHERE TenDN = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }

        //frm Quản lý tài khoản
        public bool TaoTaiKhoanfrmQL(TaiKhoanDTO tk)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO TaiKhoan (TenDN, MK, LoaiTK, TrangThai, Email, SDT, MaNV, MaKH) VALUES (@TenDN, @MK, @LoaiTK, @TrangThai, @Email, @SDT, @MaNV, @MaKH)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDN", tk.TenDN);
                cmd.Parameters.AddWithValue("@MK", tk.MK);
                cmd.Parameters.AddWithValue("@LoaiTK", tk.LoaiTK);
                cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
                cmd.Parameters.AddWithValue("@Email", tk.Email);
                cmd.Parameters.AddWithValue("@SDT", tk.SDT);
                cmd.Parameters.AddWithValue("@MaNV", (object)tk.MaNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKH", (object)tk.MaKH ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaTaiKhoan(TaiKhoanDTO tk)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"UPDATE TaiKhoan SET MK = @MK, LoaiTK = @LoaiTK, TrangThai = @TrangThai,
                                Email = @Email, SDT = @SDT, MaNV = @MaNV, MaKH = @MaKH
                                WHERE TenDN = @TenDN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDN", tk.TenDN);
                cmd.Parameters.AddWithValue("@MK", tk.MK);
                cmd.Parameters.AddWithValue("@LoaiTK", tk.LoaiTK);
                cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);
                cmd.Parameters.AddWithValue("@Email", tk.Email);
                cmd.Parameters.AddWithValue("@SDT", tk.SDT);
                cmd.Parameters.AddWithValue("@MaNV", (object)tk.MaNV ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaKH", (object)tk.MaKH ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaTaiKhoan(string tenDN)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "DELETE FROM TaiKhoan WHERE TenDN = @TenDN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDN", tenDN);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<TaiKhoanDTO> TimKiemTaiKhoan(string keyword)
        {
            List<TaiKhoanDTO> result = new List<TaiKhoanDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"SELECT * FROM TaiKhoan
                                 WHERE TenDN LIKE @Keyword
                                    OR Email LIKE @Keyword
                                    OR SDT LIKE @Keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new TaiKhoanDTO
                    {
                        TenDN = reader["TenDN"].ToString(),
                        MK = reader["MK"].ToString(),
                        LoaiTK = reader["LoaiTK"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                        Email = reader["Email"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        MaNV = reader["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaNV"]),
                        MaKH = reader["MaKH"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaKH"])
                    });
                }
            }
            return result;
        }
        public List<TaiKhoanDTO> GetAll()
        {
            List<TaiKhoanDTO> result = new List<TaiKhoanDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM TaiKhoan";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new TaiKhoanDTO
                    {
                        TenDN = reader["TenDN"].ToString(),
                        MK = reader["MK"].ToString(),
                        LoaiTK = reader["LoaiTK"].ToString(),
                        TrangThai = reader["TrangThai"].ToString(),
                        Email = reader["Email"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        MaNV = reader["MaNV"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaNV"]),
                        MaKH = reader["MaKH"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["MaKH"])
                    });
                }
            }
            return result;
        }
    }
}
