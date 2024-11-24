using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PiringPeduliWPF.View.Component
{
    /// <summary>
    /// Interaction logic for RecyclerContainer.xaml
    /// </summary>
    public partial class RecyclerContainer : UserControl
    {

        public RecyclerContainer()
        {
            InitializeComponent();
        }

        private void NumberOnlyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Gunakan regex untuk memvalidasi input hanya angka
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void QuantitiyBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (QuantityBox.Text == "Quantity")
            {
                QuantityBox.Text = string.Empty;
                QuantityBox.Foreground = new SolidColorBrush(Colors.Black);  // Warna teks ketika mulai mengetik
            }
        }

        private void QuantityBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuantityBox.Text))
            {
                QuantityBox.Text = "Quantity";  // Placeholder
                QuantityBox.Foreground = new SolidColorBrush(Colors.Gray);  // Warna placeholder
            }
        }
    }
}
