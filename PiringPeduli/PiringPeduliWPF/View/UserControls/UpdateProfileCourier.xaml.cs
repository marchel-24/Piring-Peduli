using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
    public partial class UpdateProfileCourier : UserControl
    {
        public UpdateProfileCourier()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDb"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);
            DataContext = new UpdateProfileViewModel(userService);
            //Debug.Print((UpdateProfileViewModel)DataContext.));
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is UpdateProfileViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; // Update the ViewModel
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var confirmPasswordBox = sender as PasswordBox;
            if (DataContext is UpdateProfileViewModel viewModel)
            {
                viewModel.ConfirmPassword = confirmPasswordBox.Password; // Update the ViewModel
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

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            VehicleTypeQuestionComboBox.IsDropDownOpen = !VehicleTypeQuestionComboBox.IsDropDownOpen;
        }
    }
}
