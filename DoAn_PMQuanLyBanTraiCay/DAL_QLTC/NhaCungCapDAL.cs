using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class NhaCungCapDAL
    {
        public List<NhaCungCapDTO> LayDanhSach()
        {
            List<NhaCungCapDTO> ds = new List<NhaCungCapDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = "SELECT * FROM NhaCungCap";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ds.Add(new NhaCungCapDTO
                    {
                        MaNCC = (int)rd["MaNCC"],
                        TenNCC = rd["TenNCC"].ToString(),
                        SDT = rd["SDT"].ToString(),
                        Email = rd["Email"].ToString(),
                        DiaChi = rd["DiaChi"].ToString(),
                        GhiChu = rd["GhiChu"].ToString(),
                        NgayTao = (DateTime)rd["NgayTao"]
                    });
                }
            }
            return ds;
        }

        public bool Them(NhaCungCapDTO ncc)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = "INSERT INTO NhaCungCap (TenNCC, SDT, Email, DiaChi, GhiChu) " +
                               "VALUES (@TenNCC, @SDT, @Email, @DiaChi, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@SDT", ncc.SDT);
                cmd.Parameters.AddWithValue("@Email", ncc.Email);
                cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
                cmd.Parameters.AddWithValue("@GhiChu", ncc.GhiChu);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Xoa(int maNCC)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNCC", maNCC);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CapNhat(NhaCungCapDTO ncc)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                string query = "UPDATE NhaCungCap SET TenNCC = @TenNCC, SDT = @SDT, Email = @Email, DiaChi = @DiaChi, GhiChu = @GhiChu WHERE MaNCC = @MaNCC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenNCC", ncc.TenNCC);
                cmd.Parameters.AddWithValue("@SDT", ncc.SDT);
                cmd.Parameters.AddWithValue("@Email", ncc.Email);
                cmd.Parameters.AddWithValue("@DiaChi", ncc.DiaChi);
                cmd.Parameters.AddWithValue("@GhiChu", ncc.GhiChu);
                cmd.Parameters.AddWithValue("@MaNCC", ncc.MaNCC);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public static List<NhaCungCapDTO> TimKiem(string tuKhoa)
        {
            List<NhaCungCapDTO> list = new List<NhaCungCapDTO>();
            string query = "SELECT * FROM NhaCungCap WHERE TenNCC LIKE @TuKhoa";

            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TuKhoa", "%" + tuKhoa + "%");
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new NhaCungCapDTO
                    {
                        MaNCC = Convert.ToInt32(reader["MaNCC"]),
                        TenNCC = reader["TenNCC"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        Email = reader["Email"].ToString(),
                        DiaChi = reader["DiaChi"].ToString(),
                        GhiChu = reader["GhiChu"].ToString()
                    });
                }
            }
            return list;
        }
    }
}
