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

namespace BookBuddy.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        public AdminViewModel(UserViewModel uvm, DataService DS)
        {
            _dataService = DS;
            _currentUser = uvm;
        }
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _genre;
        [ObservableProperty]
        private string _language;
        [ObservableProperty]
        private string _year;
        [ObservableProperty]
        private string _imagePath;
        private byte[] _imageBytes;


        [ObservableProperty]
        private UserViewModel _currentUser;

        public async Task InsertBookIntoDatabase()
        {

            // Read the image file as a byte array
            string connectionString = "Server=10.56.8.36;Database=DB_F23_32;User Id=DB_F23_USER_32;Password=OPENDB_32;";
            _imageBytes = File.ReadAllBytes(ImagePath);
            Microsoft.Data.SqlClient.SqlConnectionStringBuilder builder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(connectionString);
            builder.Encrypt = false;
            using (Microsoft.Data.SqlClient.SqlConnection connection = new Microsoft.Data.SqlClient.SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                string sql = "spCreateBook";

                Microsoft.Data.SqlClient.SqlCommand command = new Microsoft.Data.SqlClient.SqlCommand(sql, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Update parameters: image bytes and book title to identify the specific book
                command.Parameters.AddWithValue("@Title", Title);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@Language", Language);
                command.Parameters.AddWithValue("@Genre", Genre);
                command.Parameters.AddWithValue("@Year", Year);
                command.Parameters.AddWithValue("@Image", _imageBytes);

                command.ExecuteNonQuery();
            }
        }

        public void NullifyFields()
        {
            if (Title != "")
            {
                Title = "";
                Description = "";
                ImagePath = "";
                Genre = "";
                Language = "";
                Year = "";
            }
        }

        [RelayCommand]
        public async Task AddBookToDatabase()
        {
            await InsertBookIntoDatabase();
            NullifyFields();

        }



    }
}
