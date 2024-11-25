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
    /// Interaction logic for RecyclerView.xaml
    /// </summary>
    public partial class RecyclerView : UserControl
    {
        public RecyclerView()
        {
            InitializeComponent();
            DataContext = new RecyclerViewModel();
        }

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            QuestionComboBox.IsDropDownOpen = !QuestionComboBox.IsDropDownOpen;
        }

        private void QuestionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            string selectedOption = (string)QuestionComboBox.SelectedItem;

            if (DataContext is RecyclerViewModel viewModel)
            {
                viewModel.SelectedWaste = selectedOption;
            }
        }
    }
}
