using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.ViewModels;
using BookBuddy.Models;

namespace BookBuddy.Services
{
    public class DataService 
   
    {
        readonly IDBAccess db;
        private static readonly string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
        SqlConnection connection;
        
        public async Task<User> LogIn(string username, string password)
        {
            int userId = await VerifyCredentials(username, password);
            User user = new User();
            if (userId != 0)
            {
               user = (User) await InstantiateUser(userId);
               return user;
            }
            return user;

        }
        public async Task<int> VerifyCredentials(string username, string password)
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
                            int userid = reader.GetInt32(reader.GetOrdinal("UserId"));
                            return userid;

                        }
                        else
                            return 0;
                    }
                }
            }
        }

        public async Task<IEnumerable<User>> InstantiateUser(int userid)
        {
           return await db.LoadData<User, dynamic>("spRetrieveUser", new { UserId = userid });
        }
    }
}
