using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static PiringPeduliClass.Model.NewsModel;
using static System.Net.WebRequestMethods;

namespace PiringPeduliClass.Service
{
    public class NewsService
    {
        private readonly string apiKey;
        private readonly string baseUrl = "https://newsapi.org/v2/";

        public NewsService()
        {
            apiKey = ConfigurationManager.AppSettings["NewsApiKey"];
        }

        public async Task<List<Article>> GetTopHeadlinesAsync(string category = "food")
        {
            string url = $"{baseUrl}everything?q={category}&language=en&apiKey={apiKey}";
            Debug.WriteLine($"Requesting URL: {url}");


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                Debug.WriteLine(response);


                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(jsonResponse);
                    return newsResponse.Articles;
                }
                else
                {
                    throw new HttpRequestException($"Error fetching news: {response.StatusCode}");
                }
            }
        }
    }
}
