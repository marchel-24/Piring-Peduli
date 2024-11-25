using PiringPeduliClass.Repository;
using PiringPeduliClass.Service;
using PiringPeduliWPF.View.Component;
using PiringPeduliWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
using static PiringPeduliClass.Model.NewsModel;

namespace PiringPeduliWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    /// 
    public partial class HomeScreen : UserControl
    {
        private readonly NewsService newsService = new NewsService();

        public HomeScreen()
        {
            InitializeComponent();
            LoadNews();
            DataContext = new AccountViewModel();
        }

        private async void LoadNews()
        {
            try
            {
                List<Article> articles = await newsService.GetTopHeadlinesAsync("food");
                NewList.Children.Clear();
                foreach (var article in articles)
                {
                    var newsContainer = new NewsContainer
                    {
                        Title = string.IsNullOrWhiteSpace(article.Title) ? "No Title Available" : article.Title,
                        Description = string.IsNullOrWhiteSpace(article.Description) ? "No Description Available" : article.Description,
                        ImageUrl = article.UrlToImage ?? "https://via.placeholder.com/80",
                        ArticleUrl = article.Url
                    };
                    NewList.Children.Add(newsContainer);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error fetching news: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
