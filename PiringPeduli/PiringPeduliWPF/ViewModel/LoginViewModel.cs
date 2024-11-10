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
    public class LoginViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;
        private readonly NavigationService _navigationService;

        private string _username;
        private string _password;

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

        public ICommand LoginCommand { get; }
        public ICommand NavigateSignUpCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModeCommand(Login);
        }

        public LoginViewModel(AccountService accountService, NavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;

            LoginCommand = new ViewModeCommand(Login);
            NavigateSignUpCommand = new ViewModeCommand(NavigateSignUp);
        }

        private async void Login(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    throw new Exception("Username is required.");
                }

                // Validate if the password is provided and meets the length requirement
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("Password is required.");
                }

                // Attempt login asynchronously
                var account = await _accountService.Login(Username, Password);

                if (account != null)
                {
                    UserSessionService.Account = account;
                    _navigationService.NavigateTo("HomeScreenView");
                }
            }
            catch (Exception ex)
            {
                // Catch any unexpected exceptions and display them in a message box
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NavigateSignUp(object obj)
        {
            _navigationService.NavigateTo("SignUpView");
        }
    }
}
