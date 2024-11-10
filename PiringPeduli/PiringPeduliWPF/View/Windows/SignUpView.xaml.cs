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
            var navigationSercive = new NavigationService(this);
            DataContext = new SignUpViewModel(userService, navigationSercive);
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

            // Update pertanyaan kedua berdasarkan pilihan
            switch (selectedOption)
            {
                case "Opsi A":
                    DynamicQuestionText.Text = "Pertanyaan 2: Anda memilih Opsi A. Apa pendapat Anda?";
                    break;
                case "Opsi B":
                    DynamicQuestionText.Text = "Pertanyaan 2: Anda memilih Opsi B. Bagaimana tanggapan Anda?";
                    break;
                case "Opsi C":
                    DynamicQuestionText.Text = "Pertanyaan 2: Anda memilih Opsi C. Mengapa Anda memilih ini?";
                    break;
                default:
                    DynamicQuestionText.Text = "Pertanyaan 2: Pilih salah satu opsi di atas.";
                    break;
            }
        }

    }
}
