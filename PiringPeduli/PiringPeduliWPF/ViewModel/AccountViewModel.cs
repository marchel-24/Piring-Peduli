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

namespace PiringPeduliWPF.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly AccountService _accountService;

        private string _username;
        private string _phoneNumber;
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

        public AccountViewModel()
        {
            LoginCommand = new ViewModeCommand(Login);
            RegisterCommand = new ViewModeCommand(Register);
        }


        public AccountViewModel(AccountService accountService)
        {
            _accountService = accountService;

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


        private void Register(object obj)
        {
            _accountService.RegisterNewAccount(Username, Password, PhoneNumber, AccountType.Customer);


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
