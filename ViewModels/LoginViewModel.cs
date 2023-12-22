using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Services;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookBuddy.Views;

namespace BookBuddy.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly DataService _dataService;

        
        public LoginViewModel(UserViewModel uvm, DataService DS)
        {
            _dataService = DS;
            _currentUser = uvm;
        }
        public DataService DataService { get { return _dataService; } }

        // ObservableProperties are being set.
        [ObservableProperty]
        private UserViewModel _currentUser;
        [ObservableProperty]
        private string? _enteredUsername;
        [ObservableProperty]
        private string? _enteredPassword;

        // Instantiating library of user at verified credentials. 
        public async Task InstantiateLibrary()
        {
            List<Book> retrievedList = await _dataService.RetrieveLibrary(CurrentUser.User.UserId);
            ConvertBookToBVMAndPopulate(retrievedList);
        }

        // Converting from book objects (retrived by the dataservice) to BookViewModels that are then ready to be shown for the views that needs them.
        public void ConvertBookToBVMAndPopulate(List<Book> bookList)
        {
            if (CurrentUser != null)
            {
                foreach (Book book in bookList)
                {
                    BookViewModel bookViewModel = new(book);
                    CurrentUser.Library.Add(bookViewModel);
                }
            }
        }

        // This command is associated with the Log In button and will set properties of CurrentUser to the User properties of what was retrieved by the DataService
        [RelayCommand]
        public async Task ButtonLogIn()
        {
            await Task.Run(async () =>
            {
                User RetrievedUser = await DataService.VerifyAndInstantiate(EnteredUsername, EnteredPassword);

                if (RetrievedUser != null)
                {
                    CurrentUser.SetProperties(new(RetrievedUser));
                    EnteredUsername = "";
                    EnteredPassword = "";
                    await InstantiateLibrary();
                    // await InstantiateLibrary(); 
                }
            });

        }
    }
}
