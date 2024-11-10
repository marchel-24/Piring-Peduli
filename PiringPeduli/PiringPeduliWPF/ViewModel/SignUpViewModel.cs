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
        private string _errorMessage;

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

        public string ErrorMessage
        {
            get => _errorMessage;
            private set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand UpdateCommand {  get; }
        public ICommand DeleteCommand { get; }

        public SignUpViewModel()
        {
            LoginCommand = new ViewModeCommand(Login);
            RegisterCommand = new ViewModeCommand(Register);
        }

        public SignUpViewModel(AccountService accountService)
        {
            _accountService = accountService;

            LoginCommand = new ViewModeCommand(Login);
            RegisterCommand = new ViewModeCommand(Register);
            UpdateCommand = new ViewModeCommand(Update);
            DeleteCommand = new ViewModeCommand(Delete);
        }

        public SignUpViewModel(AccountService accountService, NavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;

            LoginCommand = new ViewModeCommand(Login);
            RegisterCommand = new ViewModeCommand(Register);
            UpdateCommand = new ViewModeCommand(Update);
            DeleteCommand = new ViewModeCommand(Delete);
        }

        private void Login(object obj)
        {
            string errorMessage;
            var account = _accountService.Login(Username, Password, out errorMessage);

            if (account != null)
            {
                // Handle successful login (e.g., navigate to a different View)
                ErrorMessage = null;
                HomeScreen homeScreen = new HomeScreen();
                homeScreen.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                // Show error message to the user in a message box
                MessageBox.Show(errorMessage, "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                ErrorMessage = errorMessage; // Optionally, set the error message to a property if it's bound to the UI
            }
        }


        private async void Register(object obj)
        {
            try
            {
                // Validate if the passwords match
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Confirm Password Failed", "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
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
                    MessageBox.Show("Registration failed, please try again.", "Sign Up Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the registration process
                MessageBox.Show($"An error occurred: {ex.Message}", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Update(object obj)
        {

            var account = new Account
            {
                Username = Username,
                Password = Password,
                PhoneNumber = PhoneNumber,
                Type = AccountType.Customer
            };

            _accountService.UpdateAccount(account);
        }

        private void Delete(object obj)
        {
            _accountService.RemoveAccountByUsername(Username);
            LoginView loginScreen = new LoginView();
            Application.Current.Windows.Equals(loginScreen);
            loginScreen.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
