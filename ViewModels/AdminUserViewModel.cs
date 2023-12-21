using BookBuddy.Models;
using BookBuddy.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuddy.ViewModels
{
    public partial class AdminUserViewModel : ObservableObject
    {

        private readonly DataService _dataService;

        public AdminUserViewModel(DataService DS)
        {
            _dataService = DS;
        }
        public DataService DataService { get { return _dataService; } }
        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private UserViewModel _selectedUser;

        private ObservableCollection<UserViewModel> _userList;
        public ObservableCollection<UserViewModel> UserList { get { return _userList; } }



        [RelayCommand]
        public async Task UpdateUser()
        {
            if (!UserList.Contains(SelectedUser) && SelectedUser.User != null)
            {
                SelectedUser.User.Username = SelectedUser.Username;
                SelectedUser.User.Email = SelectedUser.Email;
                await DataService.UpdateUser(SelectedUser.User);
            }
        }

        [RelayCommand]
        public async Task DeleteUser()
        {
            if (!UserList.Contains(SelectedUser) && SelectedUser.User != null)
            {
                await DataService.DeleteUser(SelectedUser.User);
                UserList.Remove(SelectedUser);
            }
        }
        [RelayCommand]
        public async Task SearchUsers()
        {
            List<User> userConversionList = await DataService.SearchUser(SearchText);
            foreach (User User in userConversionList)
            {
                UserViewModel userviewmodel = new(User);
                UserList.Add(userviewmodel);
            }
        }
    }
}
