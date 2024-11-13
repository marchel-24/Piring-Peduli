using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.ViewModel;
using PiringPeduliWPF.Services;
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
using System.Diagnostics;
using PiringPeduliClass.Model;

namespace PiringPeduliWPF.View.Windows
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        public SignUpView()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["PiringPeduliDB"].ConnectionString;

            // Pass the connection string to the UserRepository and UserService
            var userRepository = new AccountRepository(connectionString);
            var userService = new AccountService(userRepository);
            DataContext = new SignUpViewModel(userService);
        }

        private void BtnMinimized_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClosed_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is SignUpViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; // Update the ViewModel
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var confirmPasswordBox = sender as PasswordBox;
            if (DataContext is SignUpViewModel viewModel)
            {
                viewModel.ConfirmPassword = confirmPasswordBox.Password; // Update the ViewModel
            }
        }

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            QuestionComboBox.IsDropDownOpen = !QuestionComboBox.IsDropDownOpen;
        }


        private void QuestionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ambil pilihan yang dipilih dari ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)QuestionComboBox.SelectedItem;
            string selectedOption = selectedItem.Content.ToString();
            if (DataContext is SignUpViewModel viewModel)
            {
                viewModel.AccountTypeStr = selectedOption;
            }

            // Update pertanyaan kedua berdasarkan pilihan
            switch (selectedOption)
            {
                case "Courier":
                    DynamicQuestionText.Text = "Vehicle Type";
                    break;
                case "Customer":
                    DynamicQuestionText.Text = "Nama";
                    break;
                case "Recycler":
                    DynamicQuestionText.Text = "Pertanyaan 2: Anda memilih Opsi C. Mengapa Anda memilih ini?";
                    break;
                case "Temporary Storage":
                    DynamicQuestionText.Text = "Pertanyaan 2: Pilih salah satu opsi di atas.";
                    break;
            }
        }

        private void VehicleType_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            VehicleTypeComboBox.IsDropDownOpen = !VehicleTypeComboBox.IsDropDownOpen;
        }

        private void VehicleTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ambil pilihan yang dipilih dari ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)VehicleTypeComboBox.SelectedItem;
            string selectedOption = selectedItem.Content.ToString();

            // Update pertanyaan kedua berdasarkan pilihan
            switch (selectedOption)
            {
                case "Truck":
                    break;
                case "Car":
                    break;
                case "Motorcycle": 
                    break;
                
            }
        }

    }
}
