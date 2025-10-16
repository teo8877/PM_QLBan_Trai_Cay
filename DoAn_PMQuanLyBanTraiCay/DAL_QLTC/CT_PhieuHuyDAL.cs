
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLTC;

namespace DAL_QLTC
{
    public class CT_PhieuHuyDAL
    {
        public List<CT_PhieuHuyDTO> GetAllByMaPH(int maPH)
        {
            List<CT_PhieuHuyDTO> result = new List<CT_PhieuHuyDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM CT_PhieuHuy WHERE MaPH = @MaPH";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPH", maPH);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new CT_PhieuHuyDTO
                    {
                        MaPH = (int)reader["MaPH"],
                        MaSP = (int)reader["MaSP"],
                        SoLuong = (int)reader["SoLuong"],
                        LyDo = reader["LyDo"].ToString()
                    });
                }
            }
            return result;
        }

        public bool Insert(CT_PhieuHuyDTO ct)
        {
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "INSERT INTO CT_PhieuHuy (MaPH, MaSP, SoLuong, LyDo) VALUES (@MaPH, @MaSP, @SoLuong, @LyDo)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPH", ct.MaPH);
                cmd.Parameters.AddWithValue("@MaSP", ct.MaSP);
                cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);
                cmd.Parameters.AddWithValue("@LyDo", ct.LyDo);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<CT_PhieuHuyDTO> GetAll()
        {
            List<CT_PhieuHuyDTO> result = new List<CT_PhieuHuyDTO>();
            using (SqlConnection conn = SqlConnectionData.Connect())
            {
                conn.Open();
                string query = "SELECT * FROM CT_PhieuHuy";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new CT_PhieuHuyDTO
                    {
                        MaPH = (int)reader["MaPH"],
                        MaSP = (int)reader["MaSP"],
                        SoLuong = (int)reader["SoLuong"],
                        LyDo = reader["LyDo"].ToString()
                    });
                }
            }
            return result;
        }
    }
}