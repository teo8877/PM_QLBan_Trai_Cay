using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;
namespace DAL_QLTC
{
    public class SanPhamDAL
    {
        public List<SanPhamDTO> GetAll()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM SanPham";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SanPhamDTO
                    {
                        MaSP = (int)reader["MaSP"],
                        TenSP = reader["TenSP"].ToString(),
                        DonGia = Convert.ToDecimal(reader["DonGia"]),
                        DonViTinh = reader["DonViTinh"].ToString(),
                        GhiChu = reader["GhiChu"].ToString(),
                        NgayTao = reader["NgayTao"] == DBNull.Value ? null : (DateTime?)reader["NgayTao"]
                    });
                }
            }
            return list;
        }

        public bool Insert(SanPhamDTO sp)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"INSERT INTO SanPham (TenSP, DonGia, DonViTinh, GhiChu)
                                 VALUES (@TenSP, @DonGia, @DonViTinh, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
                cmd.Parameters.AddWithValue("@DonViTinh", (object)sp.DonViTinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChu", (object)sp.GhiChu ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(SanPhamDTO sp)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"UPDATE SanPham SET
                                    TenSP = @TenSP,
                                    DonGia = @DonGia,
                                    DonViTinh = @DonViTinh,
                                    GhiChu = @GhiChu
                                 WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                cmd.Parameters.AddWithValue("@DonGia", sp.DonGia);
                cmd.Parameters.AddWithValue("@DonViTinh", (object)sp.DonViTinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GhiChu", (object)sp.GhiChu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaSP", sp.MaSP);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int maSP)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<SanPhamDTO> Search(string keyword)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"SELECT * FROM SanPham 
                                 WHERE TenSP LIKE @keyword OR DonViTinh LIKE @keyword OR GhiChu LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new SanPhamDTO
                    {
                        MaSP = (int)reader["MaSP"],
                        TenSP = reader["TenSP"].ToString(),
                        DonGia = Convert.ToDecimal(reader["DonGia"]),
                        DonViTinh = reader["DonViTinh"].ToString(),
                        GhiChu = reader["GhiChu"].ToString(),
                        NgayTao = reader["NgayTao"] == DBNull.Value ? null : (DateTime?)reader["NgayTao"]
                    });
                }
            }
            return list;
        }
    }
}