using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.ViewModels;
using BookBuddy.Models;
using System.Security.Cryptography.X509Certificates;

namespace BookBuddy.Services
{
    public class DataService 
   
    {
        readonly IDBAccess db;
        private static readonly string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
        SqlConnection connection;
        

        public async Task LogIn(string username, string password)
        {
            bool verified = await VerifyCredentials(username, password);
            if (verified)
            {
                LoadUser();
            }
        }
        public async Task<bool> VerifyCredentials(string username, string password)
        {
            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                // connect.Open();
                using (SqlCommand command = new SqlCommand("EXEC spValidation", connection))
                {
                    //command.Parameters.AddWithValue;
                    command.Parameters.AddWithValue("@Username", username);
                    
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.GetInt32(reader.GetOrdinal("UserId"));
                                         
                        }
                            
                        else                       
                            return false;
                    }
                }
            }
        }

        public async Task<IEnumerable<User>> LoadUser(int UserId, SqlConnection connection, SqlDataReader reader)
        {
            reader.
        }
    }
}
