﻿using BookBuddy.Models;
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
        readonly DBAccess db = new();
        private static readonly string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
        SqlConnection connection;


        public async Task<bool> IfUserExists(string username, string email)
        {
            return await Task.Run(() =>
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spIfUserExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", email);

                        // Only returns one, as the column "Username" has the constraint "UNIQUE" in the SQL data table.
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                return true;
                            else
                                return false;
                        }
                    }
                }
            });
        }
        public async Task<User> VerifyAndInstantiate(string username, string password)
        {
            int userId = await VerifyCredentials(username, password);
            return await InstantiateUser(userId);
        }

        public async Task AddToLibrary(Book CurrentBook, User CurrentUser)
        {
            await db.SaveData("spAddToLibrary", new
            {
                UserId = CurrentUser.UserId,
                BookId = CurrentBook.BookId
            });
        }

        public async Task RemoveFromLibrary(Book CurrentBook, User CurrentUser)
        {
            await db.SaveData("spRemoveFromLibrary", new
            {
                UserId = CurrentUser.UserId,
                BookId = CurrentBook.BookId
            });
        }
        public async Task RateBook(Book CurrentBook, User CurrentUser)
        {
            await db.SaveData("spRateBook", new
            {
                BookId = CurrentBook.BookId,
                UserId = CurrentUser.UserId,
                Rating = CurrentBook.Rating
            });
        }


        public async Task<int> VerifyCredentials(string username, string password)
        {
            return await Task.Run(() =>
            {
                int userId = 0;
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("spVerifyCredentials", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        // Only returns one, as the column "Username" has the constraint "UNIQUE" in the SQL data table.
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

        public async Task<User?> InstantiateUser(int userId)
        {
            
            var userList = await db.LoadData<User, dynamic>("spRetrieveUser", new { UserId = userId });

            if (userId != 0)
            {
                return userList.ElementAt(0);
            }
            // we should then return an error message saying user was not found, and after having returned blankUser delete it again, so it does not take up memory.
            else
            {
                return null;
            }
        }

        public async Task CreateUser(string username, string password, string email, DateTime birthdate)
        {
            await db.SaveData("spCreateUser", new { Username = username, Password = password, Email = email, Birthdate = birthdate });
        }

        public async Task<ObservableCollection<Book>> SearchBook(Book currentBook)
        {
            ObservableCollection<Book> booklist = new();
            var books = await db.LoadData<Book, dynamic>("spSearchBook", new 
            { Title = currentBook.Title, 
                Description = currentBook.Description, 
                Genre = currentBook.Genre, Year = 
                currentBook.Year 
            });
            foreach (var book in books)
            {
                booklist.Add(book);
            }

            return booklist;

        }

        public async Task<ObservableCollection<Book>> RetrieveLibrary(int userId)
        {
            ObservableCollection<Book> tempLibrary = new();
            var books = await db.LoadData<Book, dynamic>("spMyLibrary", new
            {
                UserId = userId
            });
            foreach(var book in books)
            {
                tempLibrary.Add(book);
            }
            return tempLibrary;
        }
        public async Task<ObservableCollection<Book>> SearchBook(string searchtext)
        {
            ObservableCollection<Book> tempLibrary = new();
            var books = await db.LoadData<Book, dynamic>("spSearchBook", new
            {
                searchString = searchtext
            });
            foreach (var book in books)
            {
                tempLibrary.Add(book);
            }
            return tempLibrary;
        }
    }
}
