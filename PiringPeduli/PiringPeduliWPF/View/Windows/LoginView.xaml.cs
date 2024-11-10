using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.Services;
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
            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);
            var navigationService = new NavigationService(this);

            DataContext = new LoginViewModel(userService, navigationService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is LoginViewModel viewModel)
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
