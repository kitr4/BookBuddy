using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBuddy.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using BookBuddy.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Dynamic;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace BookBuddy.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        private readonly UserViewModel _userViewModel;
        private readonly BookViewModel _bookViewModel;
        private readonly LoginViewModel lvm;
        
        public MainViewModel ()
        {
           DataService DS = new DataService();
           lvm = new(DS);
        }


    }
 }


 
