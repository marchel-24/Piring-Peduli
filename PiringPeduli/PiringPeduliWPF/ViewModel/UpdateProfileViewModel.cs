using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.View.Windows;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PiringPeduliWPF.ViewModel
{
    public class UpdateProfileViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;

        private string _username;
        private string _password;
        private string _confirmPassword;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        //public UpdateProfileViewModel()
        //{
        //    UpdateCommand = new ViewModeCommand(Update);
        //}

        public UpdateProfileViewModel(AccountService accountService)
        {
            _accountService = accountService;
            UpdateCommand = new ViewModeCommand(Update);
        }


        private void Update(object obj)
        {
            Account updatedAccount = new Account
            {
                Username = Username,
                Password = Password,
            };
            _accountService.UpdateAccount(UserSessionService.Account.Username, updatedAccount);
        }
    }
}
