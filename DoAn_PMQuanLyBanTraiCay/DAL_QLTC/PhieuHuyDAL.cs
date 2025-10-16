using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class PhieuHuyDAL
    {
        public List<PhieuHuyDTO> GetAll()
        {
            List<PhieuHuyDTO> result = new List<PhieuHuyDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM PhieuHuy";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PhieuHuyDTO
                    {
                        MaPH = (int)reader["MaPH"],
                        NgayHuy = (DateTime)reader["NgayHuy"],
                        MaNV = (int)reader["MaNV"],
                        GhiChu = reader["GhiChu"] == DBNull.Value ? null : reader["GhiChu"].ToString()
                    });
                }
            }
            return result;
        }

        public bool Insert(PhieuHuyDTO ph)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO PhieuHuy (NgayHuy, MaNV, GhiChu) VALUES (@NgayHuy, @MaNV, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayHuy", ph.NgayHuy);
                cmd.Parameters.AddWithValue("@MaNV", ph.MaNV);
                cmd.Parameters.AddWithValue("@GhiChu", (object)ph.GhiChu ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Update(PhieuHuyDTO ph)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "UPDATE PhieuHuy SET NgayHuy = @NgayHuy, MaNV = @MaNV, GhiChu = @GhiChu WHERE MaPH = @MaPH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPH", ph.MaPH);
                cmd.Parameters.AddWithValue("@NgayHuy", ph.NgayHuy);
                cmd.Parameters.AddWithValue("@MaNV", ph.MaNV);
                cmd.Parameters.AddWithValue("@GhiChu", (object)ph.GhiChu ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Delete(int maPH)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "DELETE FROM PhieuHuy WHERE MaPH = @MaPH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPH", maPH);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public PhieuHuyDTO GetByMaPH(int maPH)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM PhieuHuy WHERE MaPH = @MaPH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPH", maPH);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new PhieuHuyDTO
                    {
                        MaPH = (int)reader["MaPH"],
                        NgayHuy = (DateTime)reader["NgayHuy"],
                        MaNV = (int)reader["MaNV"],
                        GhiChu = reader["GhiChu"] == DBNull.Value ? null : reader["GhiChu"].ToString()
                    };
                }
            }
            return null;
        }
        public List<PhieuHuyDTO> Search(string keyword)
        {
            List<PhieuHuyDTO> list = new List<PhieuHuyDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"SELECT * FROM PhieuHuy 
                                 WHERE MaPH LIKE @keyword OR NgayHuy LIKE @keyword OR GhiChu LIKE @keyword";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PhieuHuyDTO
                    {
                        MaPH = (int)reader["MaPH"],
                        NgayHuy = (DateTime)reader["NgayHuy"],
                        MaNV = (int)reader["MaNV"],
                        GhiChu = reader["GhiChu"] == DBNull.Value ? null : reader["GhiChu"].ToString()
                    });
                }
            }
            return list;
        }
        public int InsertGetID(PhieuHuyDTO ph)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO PhieuHuy (NgayHuy, MaNV, GhiChu) OUTPUT INSERTED.MaPH VALUES (@NgayHuy, @MaNV, @GhiChu)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayHuy", ph.NgayHuy);
                cmd.Parameters.AddWithValue("@MaNV", ph.MaNV);
                cmd.Parameters.AddWithValue("@GhiChu", (object)ph.GhiChu ?? DBNull.Value);
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
