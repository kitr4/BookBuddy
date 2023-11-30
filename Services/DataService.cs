using BookBuddy.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookBuddy.Services
{
    public class DataService

    {
        readonly DBAccess db = new();
        private static readonly string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
        SqlConnection connection;

        public async Task<User> LogIn(string username, string password)
        {
            int userId = await VerifyCredentials(username, password);

            return await InstantiateUser(userId);

        }


        public async Task<int> VerifyCredentials(string username, string password)
        {
            return await Task.Run(() =>
            {
                int userId = 0;
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spValidation", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    userId = reader.GetInt32(reader.GetOrdinal("UserId"));
                                }
                            }
                        }
                    }
                }
                return userId;
            });
        }

        public async Task<User> InstantiateUser(int userid)
        {
            User blankUser = new(0, "EMPTY USER", "EMPTY USER", "EMPTY@EMPTY.COM");
            var userlist = await db.LoadData<User, dynamic>("spRetrieveUser", new { UserId = userid });
            if (userid != 0)
            {
                return userlist.ElementAt(0);
            }
            else
            {
                return blankUser;
            }
        }
    }
}
