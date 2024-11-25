using PiringPeduliWPF.ViewModel;
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
    /// Interaction logic for TemporaryContainer.xaml
    /// </summary>
    public partial class TemporaryContainer : UserControl
    {
        public TemporaryContainer()
        {
            InitializeComponent();
        }

        private void NumberOnlyTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Gunakan regex untuk memvalidasi input hanya angka
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }


    }
}
