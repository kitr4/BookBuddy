using BookBuddy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.IO;
using BookBuddy.Models;
using System.Collections.ObjectModel;

namespace BookBuddy.ViewModels
{
    public partial class AdminBookViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public AdminBookViewModel(UserViewModel uvm, DataService DS)
        {
            _dataService = DS;
            _currentUser = uvm;
        }
        public DataService DataService { get { return _dataService; } }

        // SelectedItem in xaml onto this
        [ObservableProperty]
        private BookViewModel _selectedBook;
        // Bind up on searchbox
        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private string _imagePath;
        private bool _isSuccess;


        [ObservableProperty]
        private UserViewModel _currentUser;
        
        private ObservableCollection<BookViewModel> _bookList = new();
        public ObservableCollection<BookViewModel> BookList { get { return _bookList; } set { _bookList = value; } }


        public async Task<bool> InsertBookIntoDatabase()
        {
            // this goes into dataservice 
            // Read the image file as a byte array
            string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
            SelectedBook.Image = File.ReadAllBytes(ImagePath);
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            builder.Encrypt = false;
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string sql = "spCreateBook";

                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(sql, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Update parameters: image bytes and book title to identify the specific book
                command.Parameters.AddWithValue("@Title", SelectedBook.Title);
                command.Parameters.AddWithValue("@Description", SelectedBook.Description);
                command.Parameters.AddWithValue("@Language", SelectedBook.Language);
                command.Parameters.AddWithValue("@Genre", SelectedBook.Genre);
                command.Parameters.AddWithValue("@Year", SelectedBook.Year);
                command.Parameters.AddWithValue("@Image", SelectedBook.Image);

                command.ExecuteNonQuery();
                if(command.ExecuteNonQuery()!= 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void NullifyFields()
        {
            if (SelectedBook.Title != "")
            {
                SelectedBook.Title = "";
                SelectedBook.Description = "";
                ImagePath = "";
                SelectedBook.Genre = "";
                SelectedBook.Language = "";
                SelectedBook.Year = null;
            }
        }

        [RelayCommand]
        public async Task UpdateBook()
        {
            await DataService.
        }

        [RelayCommand]
        public async Task AddBookToDatabase()
        {
            _isSuccess = await InsertBookIntoDatabase();
            if (_isSuccess)
            {
                NullifyFields();
            }

        }

        [RelayCommand]
        public async Task SearchBook()
        {
            List<Book> bookConversionList = await DataService.SearchBook(SearchText);
            foreach (Book book in bookConversionList)
            {
                BookViewModel bookviewmodel = new(book);
                BookList.Add(bookviewmodel);
            }
        }


    }
}
