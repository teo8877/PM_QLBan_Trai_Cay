using DTO_QLTC;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLTC
{
    public class PhieuNhapDAL
    {
        public List<PhieuNhapDTO> GetAll()
        {
            List<PhieuNhapDTO> result = new List<PhieuNhapDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM PhieuNhap";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PhieuNhapDTO
                    {
                        MaPN = (int)reader["MaPN"],
                        NgayNhap = (DateTime)reader["NgayNhap"],
                        MaNV = (int)reader["MaNV"],
                        TongTien = (decimal)reader["TongTien"]
                    });
                }
            }
            return result;
        }

        public bool Insert(PhieuNhapDTO pn)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO PhieuNhap (NgayNhap, MaNV, TongTien) VALUES (@NgayNhap, @MaNV, @TongTien)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayNhap", pn.NgayNhap);
                cmd.Parameters.AddWithValue("@MaNV", pn.MaNV);
                cmd.Parameters.AddWithValue("@TongTien", pn.TongTien);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Update(PhieuNhapDTO pn)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "UPDATE PhieuNhap SET NgayNhap = @NgayNhap, MaNV = @MaNV, TongTien = @TongTien WHERE MaPN = @MaPN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPN", pn.MaPN);
                cmd.Parameters.AddWithValue("@NgayNhap", pn.NgayNhap);
                cmd.Parameters.AddWithValue("@MaNV", pn.MaNV);
                cmd.Parameters.AddWithValue("@TongTien", pn.TongTien);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Delete(int maPN)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "DELETE FROM PhieuNhap WHERE MaPN = @MaPN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPN", maPN);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<PhieuNhapDTO> Search(string keyword)
        {
            List<PhieuNhapDTO> result = new List<PhieuNhapDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM PhieuNhap WHERE CAST(MaPN AS VARCHAR) LIKE @keyword OR CAST(MaNV AS VARCHAR) LIKE @keyword";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new PhieuNhapDTO
                    {
                        MaPN = (int)reader["MaPN"],
                        NgayNhap = (DateTime)reader["NgayNhap"],
                        MaNV = (int)reader["MaNV"],
                        TongTien = (decimal)reader["TongTien"]
                    });
                }
            }
            return result;
        }
        public int InsertGetID(PhieuNhapDTO pn)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO PhieuNhap (NgayNhap, MaNV, TongTien) OUTPUT INSERTED.MaPN VALUES (@NgayNhap, @MaNV, @TongTien)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayNhap", pn.NgayNhap);
                cmd.Parameters.AddWithValue("@MaNV", pn.MaNV);
                cmd.Parameters.AddWithValue("@TongTien", pn.TongTien);
                return (int)cmd.ExecuteScalar();
            }

        }
        public bool UpdateTongTien(int maPN, decimal tongTien)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = @"
            UPDATE PhieuNhap
            SET TongTien = (
                SELECT SUM(ThanhTien)
                FROM CT_PhieuNhap
                WHERE MaPN = @MaPN
            )
            WHERE MaPN = @MaPN";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPN", maPN);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
