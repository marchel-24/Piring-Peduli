﻿using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;

namespace PiringPeduliWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Get the connection string from App.config
            string connectionString = "host=localhost;port=5432;Database=postgres;Username=postgres;Password=lol12345";

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);

            Debug.WriteLine(userService.GetUserById(1).Username);
            Debug.WriteLine(userService.GetUserById(1).Password);
            Debug.WriteLine(userService.GetUserById(1).AccountId);
            Debug.WriteLine(userService.GetUserById(1).Type);
        }
    }

}
