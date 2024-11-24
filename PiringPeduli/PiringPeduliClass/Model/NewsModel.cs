using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiringPeduliClass.Model
{
    public class NewsModel
    {
        public class Article
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string UrlToImage { get; set; }
        }

        public class NewsResponse
        {
            public string Status { get; set; }
            public int TotalResults { get; set; }
            public List<Article> Articles { get; set; }
        }
    }
}
