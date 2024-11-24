using PiringPeduliWPF.View.Component;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TemporaryStorage.xaml
    /// </summary>
    public partial class TemporaryStorage : UserControl
    {
        public TemporaryStorage()
        {
            InitializeComponent();
            Storage();
            TypeBox.GotFocus += TypeBox_GotFocus;
            TypeBox.LostFocus += TypeBox_LostFocus;
            BeratBox.GotFocus += BeratBox_GotFocus;
            BeratBox.LostFocus += BeratBox_LostFocus;
        }

        private void Storage()
        {
            
        }


        private void TypeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TypeBox.Text == "Type")
            {
                TypeBox.Text = string.Empty;
                TypeBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TypeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TypeBox.Text))
            {
                TypeBox.Text = "Type";
                TypeBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
        private void BeratBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (BeratBox.Text == "Weight")
            {
                BeratBox.Text = string.Empty;
                BeratBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void BeratBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BeratBox.Text))
            {
                BeratBox.Text = "Weight";
                BeratBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
