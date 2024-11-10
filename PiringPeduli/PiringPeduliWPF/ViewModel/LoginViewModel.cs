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
        public ICommand NavigateSignUpCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModeCommand(Login);
        }

        public LoginViewModel(AccountService accountService)
        {
            _accountService = accountService;

            LoginCommand = new ViewModeCommand(Login);
            NavigateSignUpCommand = new ViewModeCommand(NavigateSignUp);
        }

        public LoginViewModel(AccountService accountService, NavigationService navigationService)
        {
            _accountService = accountService;
            _navigationService = navigationService;

            LoginCommand = new ViewModeCommand(Login);
            NavigateSignUpCommand = new ViewModeCommand(NavigateSignUp);
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

        private void NavigateSignUp(object obj)
        {
            _navigationService.NavigateTo("SignUpView");
        }
    }
}
