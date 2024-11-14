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
    public class UpdateProfileRecyclerViewModel:ViewModelBase
    {
        private readonly RecyclerService _accountService;

        private string _username;
        private string _password;
        private string _recyclerName;
        private string _recycleraddress;
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

        public string RecyclerAddress
        {
            get => _recycleraddress;
            set
            {
                _recycleraddress = value;
                OnPropertyChanged(nameof(RecyclerAddress));
            }
        }

        public string RecyclerName
        {
            get => _recyclerName;
            set
            {
                _recyclerName = value;
                OnPropertyChanged(nameof(RecyclerName));
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


        public UpdateProfileRecyclerViewModel(RecyclerService accountService)
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

                if (string.IsNullOrWhiteSpace(RecyclerName))
                {
                    throw new Exception("Recycler name is required.");
                }

                if (string.IsNullOrWhiteSpace(RecyclerAddress))
                {
                    throw new Exception("Recycler address is required");
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

                Recycler updatedAccount = new Recycler
                {
                    Username = Username,
                    Password = Password,
                    Type = UserSessionService.Account.Type,
                    RecyclerName = RecyclerName,
                    RecyclerAddress = RecyclerAddress
                };

                var success = await _accountService.UpdateRecycler(UserSessionService.Account.Username, updatedAccount);
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
