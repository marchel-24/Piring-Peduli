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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PiringPeduliWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : UserControl
    {
        public UpdateProfile()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);
            DataContext = new AccountViewModel(userService);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is AccountViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; // Update the ViewModel
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent as T;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var parentFrame = FindParent<SettingsFrame>(this);
            if (parentFrame != null)
            {
                parentFrame.ContentControl.Content = new SettingsFrame();
            }
        }
    }
}
