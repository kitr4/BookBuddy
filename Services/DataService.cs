using BookBuddy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookBuddy.Services
{
    public class DataService
    {
        //private static readonly DataService _stDS = new DataService();
        private readonly IDBAccess _db;

        // this string is already on our _db object private static readonly string connectionString = "Server=10.56.8.36;Database=_db_F23_32;User Id=_db_F23_USER_32;Password=OPEN_db_32;";
        public DataService(IDBAccess DBAccess)
        {
            _db = DBAccess;
        }
        public IDBAccess DB { get { return _db; } }

        //public static DataService stDS => _stDS;
        
        // Thomas foretrækker at vi gør det på den manuelle måde, som her
        //public async Task<bool> IfUserExists(string username, string email)
        //{
        //    return await Task.Run(() =>
        //    {
        //        using (connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand("spIfUserExists", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@Username", username);
        //                command.Parameters.AddWithValue("@Password", email);

        //                // Only returns one, as the column "Username" has the constraint "UNIQUE" in the SQL data table.
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                        return true;
        //                    else
        //                        return false;
        //                }
        //            }
        //        }
        //    });
        //}


        public async Task<bool> IfUserExists(string username, string email)
        {
            var parameters = new { Username = username, Email = email };

            var result = await DB.LoadData<User, dynamic>("spIfUserExists", parameters);
            return result.Any(); // Check if there's any result, implying the user exists
        }


        public async Task<User> VerifyAndInstantiate(string username, string password)
        {
            int userId = await VerifyCredentials(username, password);
            return await InstantiateUser(userId);
        }

        public async Task AddToLibrary(Book CurrentBook, User CurrentUser)
        {
            await DB.SaveData("spAddToLibrary", new
            {
                UserId = CurrentUser.UserId,
                BookId = CurrentBook.BookId
            });
        }

        public async Task RemoveFromLibrary(Book CurrentBook, User CurrentUser)
        {
            await DB.SaveData("spRemoveFromLibrary", new
            {
                UserId = CurrentUser.UserId,
                BookId = CurrentBook.BookId
            });
        }
        public async Task RateBook(int? rating, Book CurrentBook, User CurrentUser)
        {
            await DB.SaveData("spRateBook", new
            {
                BookId = CurrentBook.BookId,
                UserId = CurrentUser.UserId,
                Rating = rating
            });
        }

        //public async Task<int> VerifyCredentials(string username, string password)
        //{
        //    return await Task.Run(() =>
        //    {
        //        int userId = 0;
        //        using (connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using (SqlCommand command = new SqlCommand("spVerifyCredentials", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@Username", username);
        //                command.Parameters.AddWithValue("@Password", password);

        //                // Only returns one, as the column "Username" has the constraint "UNIQUE" in the SQL data table.
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            userId = reader.GetInt32(reader.GetOrdinal("UserId"));
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        return userId;
        //    });
        //}
        public async Task<int> VerifyCredentials(string username, string password)
        {
            var parameters = new { Username = username, Password = password };

            var result = await DB.LoadData<int, dynamic>("spVerifyCredentials", parameters);
            int userId = result.FirstOrDefault(); 

            return userId;
        }

        public async Task<User?> InstantiateUser(int userId)
        {
            
            var userList = await DB.LoadData<User, dynamic>("spRetrieveUser", new { UserId = userId });

            if (userList.Any())
            {
                return userList.ElementAt(0);
            }
            // we should then return an error message saying user was not found, and after having returned blankUser delete it again, so it does not take up memory.
            else
            {
                return null;
            }
        }

        public async Task UpdateBook(Book book)
        {
            await DB.SaveData("spUpdateBook", new { BookId = book.BookId, Title = book.Title, Description = book.Description, Language = book.Language, Genre = book.Genre, Image = book.Image, Year = book.Year });
        }
        public async Task DeleteBook(Book book)
        {
            await DB.SaveData("spDeleteBook", new { BookId = book.BookId });
        }
        public async Task DeleteUser(User user)
        {
            await DB.SaveData("spDeleteUser", new {UserId = user.UserId});
        }
        public async Task UpdateUser(User user)
        {
            await DB.SaveData("spUpdateUser", new { UserId = user.UserId, Username = user.Username, Email = user.Email, isAdmin = user.isAdmin });
        }
        public async Task CreateAuthor(string name, string nationality)
        {
            await DB.SaveData("spCreateAuthor", new { Name = name, Nationality = nationality});
        }
        public async Task UpdateAuthor(Author author)
        {
            await DB.SaveData("spUpdateAuthor", new { AuthorId = author.AuthorId, Name = author.Name, Nationality = author.Nationality });
        }
        public async Task DeleteAuthor(Author author)
        {
            await DB.SaveData("spDeleteAuthor", new { AuthorId = author.AuthorId });
        }

        public async Task<List<User>> SearchUser(string searchtext)
        {
            List<User> userResults = new();
            var users = await DB.LoadData<User, dynamic>("spSearchUser", new
            {
                searchString = searchtext
            });
            foreach (var user in users)
            {
                userResults.Add(user);
            }
            return userResults;
        }

        public async Task CreateUser(string username, string password, string email)
        {
            await DB.SaveData("spCreateUser", new { Username = username, Password = password, Email = email});
        }

        //public async Task<ObservableCollection<Book>> SearchBook(Book currentBook)
        //{
        //    ObservableCollection<Book> booklist = new();
        //    var books = await DB.LoadData<Book, dynamic>("spSearchBook", new 
        //    { Title = currentBook.Title, 
        //        Description = currentBook.Description, 
        //        Genre = currentBook.Genre, Year = 
        //        currentBook.Year 
        //    });
        //    foreach (var book in books)
        //    {
        //        booklist.Add(book);
        //    }
        //    return booklist;
        //}

        public async Task<List<Book>> RetrieveLibrary(int userId)
        {
            List<Book> tempLibrary = new();
            var books = await DB.LoadData<Book, dynamic>("spMyLibrary", new
            {
                UserId = userId
            });
            foreach(var book in books)
            {
                tempLibrary.Add(book);
            }
            return tempLibrary;
        }
        public async Task<List<Book>> SearchBook(string searchtext)
        {
            List<Book> bookResults = new();
            var books = await DB.LoadData<Book, dynamic>("spSearchBook", new
            {
                searchString = searchtext
            });
            foreach (var book in books)
            {
                bookResults.Add(book);
            }
            return bookResults;
        }

    }
}
