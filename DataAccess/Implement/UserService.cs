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
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly string dbconnection;
        private readonly string dateformat;
        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
            dbconnection = this.configuration["ConnectionStrings:DB"];
            dateformat = this.configuration["Constants:DateFormat"];
        }

      

        public User GetUser(int id)
        {
            var user = new User();
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };

                string query = "SELECT * FROM tb_SinhVien WHERE MaSV=" + id + ";";
                command.CommandText = query;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.MaSV = (int)reader["MaSv"];
                    user.HoTenSV = (string)reader["HoTenSV"];
                    user.GioiTinh = (string)reader["GioiTinh"];

                    user.Email = (string)reader["Email"];
                    user.TaiKhoan = (string)reader["TaiKhoan"];
                    user.DienThoai = (string)reader["DienThoai"];
          
                    user.MatKhau = (string)reader["MatKhau"];


                }
            }
            return user;
        }


        public bool InsertUser(User user)
        {
            using(SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();
                string query = "SELECT COUNT(*) FROM tb_SinhVien WHERE TaiKhoan='" + user.TaiKhoan + "';";
                command.CommandText = query;
                int count = (int)command.ExecuteScalar();
                if(count > 0)
                {
                    connection.Close();
                    return false;
                }
                query = "INSERT INTO tb_SinhVien(MaSV, HoTenSV, GioiTinh, Email, TaiKhoan,  DienThoai, MatKhau) values (@ma, @ht, @gt,  @em, @acc, @sdt,  @mk);";
                command.CommandText = query;
            
                command.Parameters.Add("@ma", System.Data.SqlDbType.NVarChar).Value = user.MaSV;
                command.Parameters.Add("@ht", System.Data.SqlDbType.NVarChar).Value = user.HoTenSV;
                command.Parameters.Add("@gt", System.Data.SqlDbType.NVarChar).Value = user.GioiTinh;

                command.Parameters.Add("@em", System.Data.SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@acc", System.Data.SqlDbType.NVarChar).Value = user.TaiKhoan;
                command.Parameters.Add("@sdt", System.Data.SqlDbType.NVarChar).Value = user.DienThoai;
      
                command.Parameters.Add("@mk", System.Data.SqlDbType.NVarChar).Value = user.MatKhau;

                command.ExecuteNonQuery();

               
            }
            return true;
        }
        public bool Update(User user)
        {
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();

                string query = "SELECT COUNT(*) FROM tb_SinhVien WHERE MaSV ='" + user.MaSV + "' ;";
                command.CommandText = query;
                int count = (int)command.ExecuteScalar();
                if (count == 0)
                {
                    connection.Close();
                    return false;
                }

                query = "UPDATE tb_SinhVien SET HoTenSV = @ht, GioiTinh = @gt, Email = @em, TaiKhoan = @acc, DienThoai = @sdt, MatKhau = @mk WHERE MaSV = @ma;";
                
                command.CommandText = query;
                command.Parameters.Add("@ma", System.Data.SqlDbType.NVarChar).Value = user.MaSV;
                command.Parameters.Add("@ht", System.Data.SqlDbType.NVarChar).Value = user.HoTenSV;
                command.Parameters.Add("@gt", System.Data.SqlDbType.NVarChar).Value = user.GioiTinh;

                command.Parameters.Add("@em", System.Data.SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@acc", System.Data.SqlDbType.NVarChar).Value = user.TaiKhoan;
                command.Parameters.Add("@sdt", System.Data.SqlDbType.NVarChar).Value = user.DienThoai;
    
                command.Parameters.Add("@mk", System.Data.SqlDbType.NVarChar).Value = user.MatKhau;

                command.ExecuteNonQuery();
            }
            return true;
        }
        public bool Delete(int id)
        {
            var users = new List<User>();
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();
                string query = "SELECT COUNT(*) FROM tb_SinhVien WHERE MaSV ='" + id + "' ;";
                command.CommandText = query;
                int res = (int)command.ExecuteScalar();
                if (res == 0)
                {
                    connection.Close();
                    return false;
                }
                query = "DELETE FROM tb_SinhVien WHERE MaSV=" + id + ";";
                command.CommandText = query;
                command.ExecuteNonQuery();

                //query = "DELETE FROM tb_Lop WHERE MaSV=" + id + ";";
                //command.CommandText = query;
                //command.ExecuteNonQuery();

                // get Cart Id
                //query = "SELECT CartId FROM Carts WHERE UserId=" + id + ";";
                //command.CommandText = query;
                //var check = command.ExecuteScalar();
                //// If user has cart then delete them
                //if (check != null)
                //{
                //    query = "DELETE FROM CartItems WHERE CartId=" + res + ";";
                //    command.CommandText = query;
                //    command.ExecuteNonQuery();
                //    query = "DELETE FROM Carts WHERE CartId =" + res + ";";
                //    command.CommandText = query;
                //    command.ExecuteNonQuery();
                //}

                //query = "DELETE FROM tb_SinhVien WHERE MaSV=" + id + ";";
                //command.CommandText = query;
                //command.ExecuteNonQuery();

                connection.Close();
            }
            return true;

        }

    }
}





//    }
//}
//        public string IsUserPresent(string email, string password)
//        {
//            User user = new();
//            using (SqlConnection connection = new(dbconnection))
//            {
//                SqlCommand command = new()
//                {
//                    Connection = connection
//                };

//                connection.Open();
//                string query = "SELECT COUNT(*) FROM Users WHERE Email='" + email + "' AND Password='" + password + "';";
//                command.CommandText = query;
//                int count = (int)command.ExecuteScalar();
//                if (count == 0)
//                {
//                    connection.Close();
//                    return "";
//                }

//                query = "SELECT * FROM Users WHERE Email='" + email + "' AND Password='" + password + "';";
//                command.CommandText = query;

//                SqlDataReader reader = command.ExecuteReader();
//                while (reader.Read())
//                {
//                    user.Id = (int)reader["UserId"];
//                    user.FirstName = (string)reader["FirstName"];
//                    user.LastName = (string)reader["LastName"];
//                    user.Email = (string)reader["Email"];
//                    user.Address = (string)reader["Address"];
//                    user.Mobile = (string)reader["Mobile"];
//                    user.Password = (string)reader["Password"];
//                    user.CreatedAt = (string)reader["CreatedAt"];
//                    user.ModifiedAt = (string)reader["ModifiedAt"];
//                    user.Role = (string)reader["role"];
//                    user.UserAvt = (string)reader["UserAvt"];
//                }

//                string key = "MNU66iBl3T5rh6H52i69";
//                string duration = "60";
//                var symmetrickey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
//                var credentials = new SigningCredentials(symmetrickey, SecurityAlgorithms.HmacSha256);

//                var claims = new[]
//                {
//                    new Claim("id", user.Id.ToString()),
//                    new Claim("firstName", user.FirstName),
//                    new Claim("lastName", user.LastName),
//                    new Claim("address", user.Address),
//                    new Claim("mobile", user.Mobile),
//                    new Claim("email", user.Email),
//                    new Claim("createdAt", user.CreatedAt),
//                    new Claim("modifiedAt", user.ModifiedAt),
//                    new Claim("role", user.Role),
//                    new Claim("UserAvt", user.UserAvt)
//                };

//                var jwtToken = new JwtSecurityToken(
//                    issuer: "localhost",
//                    audience: "localhost",
//                    claims: claims,
//                    expires: DateTime.Now.AddMinutes(Int32.Parse(duration)),
//                    signingCredentials: credentials);

//                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
//            }
//            return "";
//        }
//public List<User> GetAllUser()
//{
//    var users = new List<User>();
//    //ket noi database
//    using (SqlConnection connection = new(dbconnection))
//    {
//        SqlCommand command = new()
//        {
//            Connection = connection
//        };

//        string query = "SELECT * FROM Users";
//        command.CommandText = query;

//        connection.Open();
//        SqlDataReader reader = command.ExecuteReader();
//        while (reader.Read())
//        {
//            var user = new User();
//            user.Id = (int)reader["UserId"];
//            user.FirstName = (string)reader["FirstName"];
//            user.LastName = (string)reader["LastName"];
//            user.Email = (string)reader["Email"];
//            user.Address = (string)reader["Address"];
//            user.Mobile = (string)reader["Mobile"];
//            user.Password = (string)reader["Password"];
//            user.CreatedAt = (string)reader["CreatedAt"];
//            user.ModifiedAt = (string)reader["ModifiedAt"];
//            user.Role = (string)reader["role"];
//            user.UserAvt = (string)reader["UserAvt"];
//            users.Add(user);
//        }
//    }
//    return users;
//}
//        public bool Delete(int id)
//        {
//            var users = new List<User>();
//            using (SqlConnection connection = new(dbconnection))
//            {
//                SqlCommand command = new()
//                {
//                    Connection = connection
//                };
//                connection.Open();
//                string query = "SELECT COUNT(*) FROM Users WHERE UserId ='" + id + "' ;";
//                command.CommandText = query;
//                int res = (int)command.ExecuteScalar();
//                if (res == 0)
//                {
//                    connection.Close();
//                    return false;
//                }
//                query = "DELETE FROM Orders WHERE UserId=" + id + ";";
//                command.CommandText = query;
//                command.ExecuteNonQuery();

//                query = "DELETE FROM Payments WHERE UserId=" + id + ";";
//                command.CommandText = query;
//                command.ExecuteNonQuery();

//                // get Cart Id
//                query = "SELECT CartId FROM Carts WHERE UserId=" + id + ";";
//                command.CommandText = query;
//                var check = command.ExecuteScalar();
//                // If user has cart then delete them
//                if(check != null)
//                {
//                    query = "DELETE FROM CartItems WHERE CartId=" + res + ";";
//                    command.CommandText = query;
//                    command.ExecuteNonQuery();
//                    query = "DELETE FROM Carts WHERE CartId =" + res + ";";
//                    command.CommandText = query;
//                    command.ExecuteNonQuery();
//                }

//                query = "DELETE FROM Users WHERE UserId=" + id + ";";
//                command.CommandText = query;
//                command.ExecuteNonQuery();

//                connection.Close();
//            }
//            return true;

//        }

//        public int TotalOfUsers()
//        {
//            using(SqlConnection connection = new(dbconnection))
//            {
//                SqlCommand command = new()
//                {
//                    Connection = connection
//                };
//                connection.Open();
//                string query = "SELECT COUNT(*) FROM Users";
//                command.CommandText = query;
//                int total = (int)command.ExecuteScalar();
//                return total;
//            }
//        }
//    }
//}

