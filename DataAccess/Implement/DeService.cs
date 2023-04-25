using System;
using ECommerce.API.DataAccess;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

using System.Data.SqlClient;
using ECommerce.API.DataAccess.Interface;

namespace ECommerce.API.DataAccess
{
    public class DeService : IDe 
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        private readonly string dbconnection;

        public DeService(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration;
            this.userService = userService;

            dbconnection = this.configuration["ConnectionStrings:DB"];

        }
        public bool InsertDe(De de)
        {
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();
                string query = "SELECT COUNT(*) FROM tb_De WHERE Made='" + de.MaDe + "';";
                command.CommandText = query;
                int count = (int)command.ExecuteScalar();
                if (count > 0)
                {
                    connection.Close();
                    return false;
                }

                query = "INSERT INTO tb_De (MaDe, CauHoiTL, MaLop) VALUES (@MaDe, @CauHoiTL, @MaLop)";
                command.CommandText = query;
                command.Parameters.Add("@MaDe", System.Data.SqlDbType.Int).Value = de.MaDe;
                command.Parameters.Add("@CauHoiTL", System.Data.SqlDbType.NVarChar).Value = de.CauHoiTL;
                command.Parameters.Add("@MaLop", System.Data.SqlDbType.Int).Value = de.MaLop;


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
                string query = "SELECT COUNT(*) FROM tb_De WHERE MaDe ='" + id + "' ;";
                command.CommandText = query;
                int res = (int)command.ExecuteScalar();
                if (res == 0)
                {
                    connection.Close();
                    return false;
                }
                query = "DELETE FROM tb_De WHERE MaDe=" + id + ";";
                command.CommandText = query;
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;

        }
        public bool UpdateDe(De de)
        {
            using (SqlConnection connection = new(dbconnection))
            {
                SqlCommand command = new()
                {
                    Connection = connection
                };
                connection.Open();

                string query = "SELECT COUNT(*) FROM tb_De WHERE MaDe ='" + de.MaDe + "' ;";
                command.CommandText = query;
                int count = (int)command.ExecuteScalar();
                if (count == 0)
                {
                    connection.Close();
                    return false;
                }

                query = "UPDATE tb_De SET CauHoiTL=@CauHoiTL WHERE Made=@MaDe;";
                command.CommandText = query;
                command.Parameters.Add("@MaDe", System.Data.SqlDbType.Int).Value = de.MaDe;
                command.Parameters.Add("@CauHoiTL", System.Data.SqlDbType.NVarChar).Value = de.CauHoiTL;
               

                command.ExecuteNonQuery();
            }
            return true;
        }

        //public List<Review> GetProductReviews(int productId)
        //{
        //    var reviews = new List<Review>();
        //    using (SqlConnection connection = new(dbconnection))
        //    {
        //        SqlCommand command = new()
        //        {
        //            Connection = connection
        //        };

        //        string query = "SELECT * FROM Reviews WHERE ProductId=" + productId + ";";
        //        command.CommandText = query;

        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            var review = new Review()
        //            {
        //                Id = (int)reader["ReviewId"],
        //                Value = (string)reader["Review"],
        //                CreatedAt = (string)reader["CreatedAt"]
        //            };

        //            var userid = (int)reader["UserId"];
        //            review.User = userService.GetUser(userid);

        //            var productid = (int)reader["ProductId"];
        //            review.Product = productService.GetProduct(productid);

        //            reviews.Add(review);
        //        }
        //        connection.Close();
        //    }
        //    return reviews;
        //}
    }
}

