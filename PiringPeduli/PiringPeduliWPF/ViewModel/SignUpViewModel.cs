using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PiringPeduliWPF.Services;

namespace PiringPeduliWPF.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;
        private readonly NavigationService _navigationService;

        private string _username;
        private string _phoneNumber;
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

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public ICommand RegisterCommand { get; }

        public SignUpViewModel()
        {
            RegisterCommand = new ViewModeCommand(Register);
        }


        public SignUpViewModel(AccountService accountService, NavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;

            RegisterCommand = new ViewModeCommand(Register);
        }


        private async void Register(object obj)
        {
            try
            {
                // Validate if the username is provided
                if (string.IsNullOrWhiteSpace(Username))
                {
                    throw new Exception("Username is required.");
                }

                // Validate if the phone number is provided
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                {
                    throw new Exception("Phone number is required.");
                }

                // Validate if the password is provided and meets the length requirement
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("Password is required.");
                }

                if (Password.Length < 8)
                {
                    throw new Exception("Password must be at least 8 characters long.");
                }

                // Validate if the confirm password matches the password
                if (Password != ConfirmPassword)
                {
                    throw new Exception("Confirm Password Failed");
                }
                var account = await _accountService.GetUserByUsernameAsync(Username);

                if (account != null) 
                {
                    throw new Exception("Username already exist");
                }
                // Proceed with registration logic
                var success = await _accountService.RegisterNewAccountAsync(Username, Password, PhoneNumber, AccountType.Customer);

                if (success)
                {
                    // After successful registration, navigate to the login view
                    _navigationService.NavigateTo("LoginView");
                }
                else
                {
                    
                    throw new Exception("Registration failed, please try again.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the registration process
                MessageBox.Show($"An error occurred: {ex.Message}", "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
