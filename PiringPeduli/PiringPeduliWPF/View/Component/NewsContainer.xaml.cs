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

namespace PiringPeduliWPF.View.Component
{
    /// <summary>
    /// Interaction logic for NewsContainer.xaml
    /// </summary>
    public partial class NewsContainer : UserControl
    {
        public NewsContainer()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(NewsContainer),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty DeskripsiProperty =
            DependencyProperty.Register(
                "Description",
                typeof(string),
                typeof(NewsContainer),
                new PropertyMetadata(string.Empty));

        public string Description
        {
            get => (string)GetValue(DeskripsiProperty);
            set => SetValue(DeskripsiProperty, value);
        }

        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register(
                "ImageUrl",
                typeof(string),
                typeof(NewsContainer),
                new PropertyMetadata(string.Empty));

        public string ImageUrl
        {
            get => (string)GetValue(ImageUrlProperty);
            set => SetValue(ImageUrlProperty, value);
        }

        public static readonly DependencyProperty ArticleUrlProperty =
            DependencyProperty.Register(
                "ArticleUrl",
                typeof(string),
                typeof(NewsContainer),
                new PropertyMetadata(string.Empty));

        public string ArticleUrl
        {
            get => (string)GetValue(ArticleUrlProperty);
            set => SetValue(ArticleUrlProperty, value);
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ArticleUrl))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = ArticleUrl,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Unable to open link: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("URL is not available.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
