using BookBuddy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BookBuddy.Services
{
    public class DataService
    {
        // DataService depending on an object that can perform databaseoperation. In this case, it will be of the class DBAccess.
        // Constructor injecting allows us to easily swap out DBAccess with another, should the need arise.
        private readonly IDBAccess _db;
        public DataService(IDBAccess DBAccess)
        {
            _db = DBAccess;
        }
        public IDBAccess DB { get { return _db; } }

        //This might seem confusing, but here we are assigning a backingfield of datatype string to hold a connectionstring to our database.
        //This will be only be used in examples of showing how you would go about retrieving and creating objects without the use of the NuGet package Z.Dapper
        private readonly string _connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;TrustServerCertificate=true;";
        // TO-DO: Create another IDBAccess class that has these "traditional" ways of doing it
        SqlConnection connection = new();
        // The examples are the following, and are used on LoginViewModel when user has been validated.

        public async Task<List<BookList>> RetrieveListsForUser(int userId)
        {
            return await Task.Run(() =>
            {
                List<BookList> listOfLists = new List<BookList>();

                using (connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spRetrieveLists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int listid = reader.GetInt32(reader.GetOrdinal("ListId"));
                                string listName = reader.GetString(reader.GetOrdinal("Name"));
                                listOfLists.Add(new BookList(listid, listName));
                            }
                        }
                    }
                }
                return listOfLists;
            });
        }

        public async Task<List<Book>> PopulateBooksForList(int userId, int listId)
        {
            return await Task.Run(() =>
            {
                List<Book> bookList = new List<Book>();
                SqlConnection connection = new();
                using (connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spPopulateList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ListId", listId);

                        // The reader.GetBytes calls for an overload method, requiring numbers to know how much to load at once, for each time it reads, and here buffer being a fixed sized byte array holding 4096 bytes. 
                        // There will be read 4096 bytes at a time

                        List<byte> imageBytes = new List<byte>();
                        long startIndex = 0;
                        long bufferSize = 4096;
                        byte[] buffer = new byte[bufferSize];

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // This loop will continue until the full image has been read, and then it will break out of the do-while loop and continue to read columns.
                                do
                                {
                                    long bytesRead = reader.GetBytes(reader.GetOrdinal("Image"), startIndex, buffer, 0, (int)bufferSize);
                                    startIndex += bytesRead;

                                    imageBytes.AddRange(buffer.Take((int)bytesRead));
                                }
                                while (startIndex < reader.GetBytes(reader.GetOrdinal("Image"), 0, null, 0, 0));


                                int bookId = reader.GetInt32(reader.GetOrdinal("BookId"));
                                string title = reader.GetString(reader.GetOrdinal("Title"));
                                string description = reader.GetString(reader.GetOrdinal("Description"));
                                string genre = reader.GetString(reader.GetOrdinal("Genre"));
                                int year = reader.GetInt32(reader.GetOrdinal("Year"));
                                string language = reader.GetString(reader.GetOrdinal("Language"));
                                int rating = reader.GetInt32(reader.GetOrdinal("Rating"));
                                byte[] image = imageBytes.ToArray();
                                // Instantiation time. Adding to booklist, and then it reads the next entity in the database.
                                Book book = new Book(bookId, genre, title, description, year, language, rating, image);
                                bookList.Add(book);
                            }
                        }
                    }
                }
                return bookList;
            });
        }
    

    public async Task<bool> IfUserExists(string username, string email)
        {
            var parameters = new { Username = username, Email = email };
            var result = await DB.LoadData<User, dynamic>("spIfUserExists", parameters);
            return result.Any(); // Check if there's any result, implying the user exists
        }
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
            else
            {
                return null;
            }
        }

        public async Task<User> VerifyAndInstantiate(string username, string password)
        {
            int userId = await VerifyCredentials(username, password);
            return await InstantiateUser(userId);
        }
        public async Task<List<Book>> RetrieveLibrary(int userId)
        {
            List<Book> tempLibrary = new();
            var books = await DB.LoadData<Book, dynamic>("spMyLibrary", new
            {
                UserId = userId
            });
            foreach (var book in books)
            {
                tempLibrary.Add(book);
            }
            return tempLibrary;
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

        public async Task UpdateBook(Book book)
        {
            await DB.SaveData("spUpdateBook", new { BookId = book.BookId, Title = book.Title, Description = book.Description, Language = book.Language, Genre = book.Genre, Image = book.Image, Year = book.Year });
        }
        public async Task DeleteBook(Book book)
        {
            await DB.SaveData("spDeleteBook", new { BookId = book.BookId });
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
        public async Task DeleteUser(User user)
        {
            await DB.SaveData("spDeleteUser", new {UserId = user.UserId});
        }
        public async Task UpdateUser(User user)
        {
            await DB.SaveData("spUpdateUser", new { UserId = user.UserId, Username = user.Username, Email = user.Email, isAdmin = user.isAdmin });
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
            await DB.SaveData("spCreateUser", new { Username = username, Password = password, Email = email });
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
        public async Task<List<Author>> SearchAuthor(string searchtext)
        {
            List<Author> authorResults = new();
            var authors = await DB.LoadData<Author, dynamic>("spSearchAuthor", new
            {
                searchString = searchtext
            });
            foreach (var author in authors)
            {
                authorResults.Add(author);
            }
            return authorResults;
        }
    }
}
