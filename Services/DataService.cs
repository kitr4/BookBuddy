using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.Services
{
    public class DataService
    {
        private static readonly string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
        SqlConnection connection = new SqlConnection(connectionString);


        public async Task<bool> VerifyCredentials(string username, string password)
        {
            using (connection)
            {
                connection.Open();
                // connect.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM users WHERE UserName = @UserName AND PassWord = @PassWord", connection))
                {
                    //command.Parameters.AddWithValue;
                    command.Parameters.AddWithValue("@UserName", username);
                    
                    command.Parameters.AddWithValue("@PassWord", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
