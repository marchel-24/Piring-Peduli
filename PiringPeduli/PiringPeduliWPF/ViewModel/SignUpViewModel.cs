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
        private readonly CourierService _courierService;

        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _accountTypeStr;
        private string _vehicleTypestr;
        private string _courierName;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string AccountTypeStr
        {
            get => _accountTypeStr;
            set
            {
                _accountTypeStr = value;
                OnPropertyChanged(nameof(AccountType));
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

        public string VehicleTypestr
        {
            get => _vehicleTypestr;
            set
            {
                _vehicleTypestr = value;
                OnPropertyChanged(nameof(VehicleType));
            }
        }

        public string CourierName
        {
            get => _courierName;
            set
            {
                _courierName = value;
                OnPropertyChanged(nameof(CourierName));
            }
        }


        public ICommand RegisterCommand { get; }

        public SignUpViewModel()
        {
            RegisterCommand = new ViewModeCommand(Register);
        }


        public SignUpViewModel(AccountService accountService, CourierService courierService)
        {
            _accountService = accountService;
            _courierService = courierService;

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

                // Validate if the password is provided and meets the length requirement
                if (string.IsNullOrWhiteSpace(Password))
                {
                    throw new Exception("Password is required.");
                }

                if (string.IsNullOrWhiteSpace(AccountTypeStr))
                {
                    throw new Exception("Account type is required.");
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

                if (string.IsNullOrWhiteSpace(VehicleTypestr))
                {
                    throw new Exception("Vehicle type gak ada");
                }

                var account = await _accountService.GetUserByUsernameAsync(Username);

                if (account != null) 
                {
                    throw new Exception("Username already exist");
                }
                // Proceed with registration logic
                if (AccountTypeStr == "Temporary Storage")
                {
                    AccountTypeStr = "TemporaryStorage";
                }else if (AccountTypeStr == "Courier")
                {
                    AccountTypeStr = "Courier";
                }

                AccountType AccountType = (AccountType)Enum.Parse(typeof(AccountType), AccountTypeStr);
                var successmadeAccount = await _accountService.RegisterNewAccountAsync(Username, Password, AccountType);

                if (successmadeAccount)
                {
                    VehicleType vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), VehicleTypestr);
                    Courier createdAccount = new Courier { Name = CourierName, Vehicle = vehicleType };

                    var successmadeCourier = await _courierService.CreateCourierAsync(createdAccount, Username);
                    // After successful registration, navigate to the login view

                    if (successmadeCourier)
                    {

                        MessageBox.Show($"Sign up done, navigate to Login", "Sign Up Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.NavigateTo("LoginView");
                    }
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
