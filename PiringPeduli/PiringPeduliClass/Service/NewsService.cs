using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using static PiringPeduliClass.Model.NewsModel;

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
            string categoryQuery = string.IsNullOrEmpty(category) ? "food" : category;
            string url = $"{baseUrl}everything?q={categoryQuery}&language=en&apiKey={apiKey}";
            Debug.WriteLine($"Constructed URL: {url}");

            using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(300) })
            {
                client.DefaultRequestHeaders.Add("User-Agent", "PiringPeduliApp/1.0");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Response Status: {response.StatusCode}");
                    Debug.WriteLine($"Response Content: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(responseContent);
                        return newsResponse.Articles;
                    }
                    else
                    {
                        throw new HttpRequestException($"Error fetching news: {response.StatusCode}, Content: {responseContent}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
