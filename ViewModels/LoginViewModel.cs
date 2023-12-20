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

        [ObservableProperty]
        private UserViewModel _currentUser;

        public DataService DataService { get { return _dataService; } }

        [ObservableProperty]
        private string? _enteredUsername;
        [ObservableProperty]
        private string? _enteredPassword;


        //public async Task LogIn(string username, string password)
        //{
        //    await Task.Run(async () =>
        //    {
        //        User RetrievedUser = await DataService.VerifyAndInstantiate(username, password);
        //        if (RetrievedUser != null)
        //        {
        //            CurrentUser.SetProperties(new(RetrievedUser));
        //            EnteredUsername = "";
        //            EnteredPassword = "";
        //            await InstantiateLibrary();
        //        }
        //    });
        //}
       

        public async Task InstantiateLibrary()
        {
            List<Book> retrievedList = await _dataService.RetrieveLibrary(CurrentUser.User.UserId);
            ConvertBookToBVMAndPopulate(retrievedList);
        }
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
