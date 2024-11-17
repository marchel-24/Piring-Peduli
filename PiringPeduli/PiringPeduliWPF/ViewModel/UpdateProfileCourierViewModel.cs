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
    public class UpdateProfileCourierViewModel : ViewModelBase
    {
        private readonly CourierService _accountService;

        private string _username;
        private string _password;
        private string _courierName;
        private string _vehicleTypeStr;
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

        public string VehicleTypeStr
        {
            get => _vehicleTypeStr;
            set
            {
                _vehicleTypeStr = value;
                OnPropertyChanged(nameof(VehicleTypeStr));
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


        public UpdateProfileCourierViewModel(CourierService accountService)
        {
            _accountService = accountService;
            UpdateCommand = new ViewModeCommand(Update);
            DeleteCommand = new ViewModeCommand(Delete);
        }


        private async void Update(object obj)
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

                if (string.IsNullOrWhiteSpace(CourierName))
                {
                    throw new Exception("Courier name is required.");
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

                if (Username != UserSessionService.Account.Username)
                {
                    if (account != null)
                    {
                        throw new Exception("Username already exist");
                    }
                }

                if (Password == UserSessionService.Account.Password)
                {
                    throw new Exception("Please change your password");
                }

                

                VehicleType vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), VehicleTypeStr);

                Courier updatedAccount = new Courier
                {
                    Username = Username,
                    Password = Password,
                    Type = UserSessionService.Account.Type,
                    Name = CourierName,
                    Vehicle = vehicleType
                };

                var success = await _accountService.UpdateCourier(UserSessionService.Account.Username, updatedAccount);
                if (success)
                {
                    MessageBox.Show($"Update done, navigate to Login", "Update Account Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserSessionService.LogOut();
                    NavigationService.NavigateTo("LoginView");
                }
                else
                {
                    throw new Exception("Update profile failed, please try again.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the registration process
                MessageBox.Show($"An error occurred: {ex.Message}", "Update Account Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void Delete(object obj)
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

                // Validate if the confirm password matches the password
                if (Password != ConfirmPassword)
                {
                    throw new Exception("Confirm Password Failed");
                }

                if (Username != UserSessionService.Account.Username || Password != UserSessionService.Account.Password)
                {
                    throw new Exception("Validation failed");
                }
                var success = await _accountService.DeleteCourier(Username);
                
                if (success)
                {
                    MessageBox.Show($"Delete done, navigate to Login", "Delete Account Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                    UserSessionService.LogOut();
                    NavigationService.NavigateTo("LoginView");
                }
                else
                {
                    throw new Exception("Delete profile failed, please try again.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the registration process
                MessageBox.Show($"An error occurred: {ex.Message}", "Delete Account Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
