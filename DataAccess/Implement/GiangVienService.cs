using System;
using ECommerce.API.DataAccess;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerce.API.DataAccess
{
    public class GiangVienService : IGiangVien
    {
        private readonly IConfiguration configuration;

        private readonly string dbconnection;

        public GiangVienService(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DB"];

        }



        public GiangVien GetGiangVien(int id)
        {
            var giangvien = new GiangVien();
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };

                string query = "SELECT COUNT(*) FROM tb_GiangVien WHERE MaGV='" + giangvien.MaGV + "';";
                command.CommandText = query;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    giangvien.MaGV = (int)reader["MaGV"];
                    giangvien.HoTenGV = (string)reader["HoTenGV"];
                    giangvien.DienThoai = (string)reader["DienThoai"];

                    giangvien.Email = (string)reader["Email"];
                    giangvien.DiaChi = (string)reader["DiaChi"];
                    giangvien.TaiKhoan = (string)reader["TaiKhoan"];
                    giangvien.MatKhau = (string)reader["MatKhau"];
                   


                }
            }
            return giangvien;
        }

        public bool Update(GiangVien gv)
        {
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();

                string query = "SELECT COUNT(*) FROM tb_GiangVien WHERE MaGV='" + gv.MaGV + "';";
                command.CommandText = query;
                int count = (int)command.ExecuteScalar();
                if (count == 0)
                {
                    connection.Close();
                    return false;
                }

                query = "UPDATE tb_GiangVien SET HoTenGV = @ma, DienThoai = @dt, Email = @em, DiaChi = @dc, TaiKhoan = @tk, MatKhau = @mk WHERE MaGV = @ma";


                command.CommandText = query;
                command.Parameters.Add("@ma", System.Data.SqlDbType.NVarChar).Value = gv.MaGV;
                command.Parameters.Add("@ht", System.Data.SqlDbType.NVarChar).Value = gv.HoTenGV;
                command.Parameters.Add("@dt", System.Data.SqlDbType.NVarChar).Value = gv.DienThoai;

                command.Parameters.Add("@em", System.Data.SqlDbType.NVarChar).Value = gv.Email;
                command.Parameters.Add("@dc", System.Data.SqlDbType.NVarChar).Value = gv.DiaChi;
                command.Parameters.Add("@tk", System.Data.SqlDbType.NVarChar).Value = gv.TaiKhoan;
                command.Parameters.Add("@mk", System.Data.SqlDbType.NVarChar).Value = gv.MatKhau;
              

                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool Delete(int id)
        {
            
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();
                string query = "SELECT COUNT(*) FROM tb_GiangVien WHERE MaGV ='" + id + "' ;";
                command.CommandText = query;
                int res = (int)command.ExecuteScalar();
                if (res == 0)
                {
                    connection.Close();
                    return false;
                }
                query = "DELETE FROM tb_GiangVien WHERE MaGV=" + id + ";";
                command.CommandText = query;
                command.ExecuteNonQuery();


                connection.Close();
            }
            return true;

        }
        public bool InsertGiangVien(GiangVien gv)
            {
                using (SqlConnection connection = new(dbconnection))
                {
                    SqlCommand command = new()
                    {
                        Connection = connection
                    };
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM tb_GiangVien WHERE MaGV='" + gv.MaGV + "';";
                    command.CommandText = query;
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        connection.Close();
                        return false;
                    }
                    query = "INSERT INTO tb_GiangVien(MaGV, HoTenGV, DienThoai, Email, DiaChi,  TaiKhoan, MatKhau) values (@ma, @ht, @dt,  @em, @dc, @tk, @mk);";
                    command.CommandText = query;

                    command.Parameters.Add("@ma", System.Data.SqlDbType.NVarChar).Value = gv.MaGV;
                    command.Parameters.Add("@ht", System.Data.SqlDbType.NVarChar).Value = gv.HoTenGV;
                    command.Parameters.Add("@dt", System.Data.SqlDbType.NVarChar).Value = gv.DienThoai;

                    command.Parameters.Add("@em", System.Data.SqlDbType.NVarChar).Value = gv.Email;
                    command.Parameters.Add("@dc", System.Data.SqlDbType.NVarChar).Value = gv.DiaChi;
                    command.Parameters.Add("@tk", System.Data.SqlDbType.NVarChar).Value = gv.TaiKhoan;
                    command.Parameters.Add("@mk", System.Data.SqlDbType.NVarChar).Value = gv.MatKhau;
                  

                    command.ExecuteNonQuery();


                }
                return true;
            }
        }
    }
