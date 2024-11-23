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

        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _accountTypeStr;
        private string _vehicleTypestr;
        private string _courierName;
        private string _customername;
        private string _customeraddress;
        private string _customerinstance;
        private string _recyclername;
        private string _recycleraddress;
        private string _storagename;
        private string _storageaddress;
        private double _lat;
        private double _lon;

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

        public string CustomerName
        {
            get => _customername;
            set
            {
                _customername = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        public string CustomerAddress
        {
            get => _customeraddress;
            set
            {
                _customeraddress = value;
                OnPropertyChanged(nameof(CustomerAddress));
            }
        }

        public string CustomerInstance
        {
            get => _customerinstance;
            set
            {
                _customerinstance = value;
                OnPropertyChanged(nameof(CustomerInstance));    
            }
        }

        public string RecyclerName
        {
            get => _recyclername;
            set
            {
                _recyclername = value;
                OnPropertyChanged(nameof(RecyclerName));
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

        public string TemporaryStorageName
        {
            get => _storagename;
            set
            {
                _storagename = value;
                OnPropertyChanged(nameof(TemporaryStorageName));
            }
        }

        public string TemporaryStorageAddress
        {
            get => _storageaddress;
            set
            {
                _storageaddress = value;
                OnPropertyChanged(nameof(TemporaryStorageAddress));
            }
        }


        public ICommand RegisterCommand { get; }

        public SignUpViewModel()
        {
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

                var account = await DatabaseService.accountService.GetUserByUsernameAsync(Username);

                if (account != null) 
                {
                    throw new Exception("Username already exist");
                }

                // Proceed with registration logic
                if (AccountTypeStr == "Temporary Storage")
                {
                    AccountTypeStr = "TemporaryStorage";
                    if (string.IsNullOrWhiteSpace(TemporaryStorageName))
                    {
                        throw new Exception("Temporary Storgae name is required");
                    }

                    if (string.IsNullOrWhiteSpace(TemporaryStorageAddress))
                    {
                        throw new Exception("Temporary Storage address is required");
                    }
                }
                else if (AccountTypeStr == "Courier")
                {
                    AccountTypeStr = "Courier";
                    if (string.IsNullOrWhiteSpace(VehicleTypestr))
                    {
                        throw new Exception("Vehicle type is required");
                    }

                    if (string.IsNullOrWhiteSpace(CourierName))
                    {
                        throw new Exception("Courier name is required");
                    }
                }
                else if (AccountTypeStr == "Customer")
                {
                    if (string.IsNullOrWhiteSpace(CustomerName))
                    {
                        throw new Exception("Customer name is required");
                    }

                    if (string.IsNullOrWhiteSpace(CustomerAddress))
                    {
                        throw new Exception("Customer address is required");
                    }

                    if (string.IsNullOrWhiteSpace(CustomerInstance))
                    {
                        throw new Exception("Customer instance is required");
                    }
                }
                else if (AccountTypeStr == "Recycler")
                {
                    if (string.IsNullOrWhiteSpace(RecyclerName))
                    {
                        throw new Exception("Recycler name is required");
                    }

                    if (string.IsNullOrWhiteSpace(RecyclerAddress))
                    {
                        throw new Exception("Recycer address is required");
                    }
                }

                AccountType AccountType = (AccountType)Enum.Parse(typeof(AccountType), AccountTypeStr);
                var successmadeAccount = await DatabaseService.accountService.RegisterNewAccountAsync(Username, Password, AccountType);

                if (successmadeAccount)
                {
                    if (AccountType == AccountType.Courier)
                    {
                        VehicleType vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), VehicleTypestr);
                        Courier createdAccount = new Courier { Name = CourierName, Vehicle = vehicleType };

                        var successmadeCourier = await DatabaseService.courierService.CreateCourierAsync(createdAccount, Username);
                        // After successful registration, navigate to the login view

                        if (successmadeCourier)
                        {

                            MessageBox.Show($"Sign up done, registered as Courier", "Sign Up Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.NavigateTo("LoginView");
                        }
                    }

                    else
                    if (AccountType == AccountType.Customer)
                    {
                        Customer createdAccount = new Customer
                        {
                            CustomerName = CustomerName,
                            CustomerAddress = CustomerAddress,
                            CustomerInstance = CustomerInstance
                        };

                        var successmadeCustomer = await DatabaseService.customerService.CreateCustomerAsync(Username, createdAccount);

                        if (successmadeCustomer)
                        {

                            MessageBox.Show($"Sign up done, registered as Customer", "Sign Up Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.NavigateTo("LoginView");
                        }
                    }
                    else
                    if (AccountType == AccountType.Recycler)
                    {
                        Recycler createdAccount = new Recycler
                        {
                            RecyclerName = RecyclerName,
                            RecyclerAddress = RecyclerAddress
                        };

                        var successmadeRecycler = await DatabaseService.recyclerService.CreateRecyclerAsync(createdAccount, Username);

                        if (successmadeRecycler)
                        {
                            MessageBox.Show($"Sign up done, registered as Recycler", "Sign Up Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.NavigateTo("LoginView");
                        }
                    }
                    else
                    if (AccountType == AccountType.TemporaryStorage)
                    {
                        TemporaryStorage createdAccount = new TemporaryStorage
                        {
                            StorageName = TemporaryStorageName,
                            StorageAddress = TemporaryStorageAddress
                        };

                        var successmadeTemporary = await DatabaseService.temporaryStorageService.CreateTemporaryStorageAsync(createdAccount, Username);

                        if (successmadeTemporary)
                        {
                            MessageBox.Show($"Sign up done, registered as Temporary Storage", "Sign Up Succeed", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.NavigateTo("LoginView");
                        }
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
