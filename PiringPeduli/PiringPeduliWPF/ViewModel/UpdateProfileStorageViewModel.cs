using PiringPeduliClass.Model;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace PiringPeduliWPF.ViewModel
{
    public class UpdateProfileStorageViewModel:ViewModelBase
    {
        private readonly TemporaryStorageService _accountService;

        private string _username;
        private string _password;
        private string _storagename;
        private string _storageaddress;
        private string _confirmPassword;
        private double? lat;
        private double? lon;

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

        public string StorageName
        {
            get => _storagename;
            set
            {
                _storagename = value;
                OnPropertyChanged(nameof(StorageName));
            }
        }

        public string StorageAddress
        {
            get => _storageaddress;
            set
            {
                _storageaddress = value;
                OnPropertyChanged(nameof(StorageAddress));
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

        public double? Lat
        {
            get => lat;
            set
            {
                lat = value;
                OnPropertyChanged(nameof(Lat));
            }
        }

        public double? Lon
        {
            get => lon;
            set
            {
                lon = value;
                OnPropertyChanged(nameof(Lon));
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }


        public UpdateProfileStorageViewModel(TemporaryStorageService accountService)
        {
            _accountService = accountService;
            UpdateCommand = new ViewModeCommand(Update);
            //DeleteCommand = new ViewModeCommand(Delete);
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
                if (string.IsNullOrWhiteSpace(StorageName))
                {
                    throw new Exception("Storage name is required");
                }

                if (string.IsNullOrWhiteSpace(StorageAddress))
                {
                    throw new Exception("Storage address is required");
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

                if (Lat == null || Lon == null)
                {
                    throw new Exception("Please choose a location");
                }

                TemporaryStorage updatedAccount = new TemporaryStorage
                {
                    Username = Username,
                    Password = Password,
                    Type = UserSessionService.Account.Type,
                    StorageName = StorageName,
                    StorageAddress = StorageAddress,
                    Lat = (double)Lat,
                    Lon = (double)Lon
                };

                var success = await _accountService.UpdateTempStorage(UserSessionService.Account.Username, updatedAccount);
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
                MessageBox.Show($"An error occurred: {ex.Message}", "Update Account Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
