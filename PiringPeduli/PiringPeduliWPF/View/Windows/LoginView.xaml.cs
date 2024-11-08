using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PiringPeduliWPF.View.Windows
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);

            DataContext = new AccountViewModel(userService);
        }

        private void LogintoSignUp_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the SignUp window
            SignUp signUpWindow = new SignUp();

            // Show the SignUp window
            signUpWindow.Show();

            // Optionally close or hide the current Login window
            this.Close(); // or this.Hide(); if you want to just hide it
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is AccountViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; // Update the ViewModel
            }
        }

        private void BtnMinimized_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClosed_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
