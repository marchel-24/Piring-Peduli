using PiringPeduliClass.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PiringPeduliClass.Model.NewsModel;

namespace PiringPeduliWPF.ViewModel
{
    public class NewsViewModel : ViewModelBase
    {
        private readonly NewsService newsService;
        public ObservableCollection<Article> Articles { get; set; }

        public NewsViewModel()
        {
            newsService = new NewsService();
            Articles = new ObservableCollection<Article>();
            LoadNews();
        }

        private async void LoadNews()
        {
            try
            {
                var articles = await newsService.GetTopHeadlinesAsync("food");
                foreach (var article in articles)
                {
                    Articles.Add(article);
                }
            }
            catch
            {
                // Handle errors here
            }
        }
    }
}
