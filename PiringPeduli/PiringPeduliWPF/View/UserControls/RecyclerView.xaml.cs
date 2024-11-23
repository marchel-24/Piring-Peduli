using PiringPeduliWPF.View.Component;
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
            LoadSorted();
        }

        private void ArrowIcon_Click(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Arrow icon clicked!");
            QuestionComboBox.IsDropDownOpen = !QuestionComboBox.IsDropDownOpen;
        }

        private void LoadSorted()
        {
            var Data = new[]
            {
                new{TypeSorted = "Liquid", Size = "Small", Address = "Jalan Duku"},
                new{TypeSorted = "Leaves", Size = "Large", Address = "Jalan Duku"}
            };

            foreach (var item in Data)
            {
                var container = new RecyclerContainer
                {
                    DataContext = item
                };
                SortedList.Children.Add(container);
            }
        }
    }
}
