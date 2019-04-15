using System;
using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Azure.CognitiveServices.Search.WebSearch.Models;
using Microsoft.Azure.CognitiveServices.Search.NewsSearch;
using Microsoft.Azure.CognitiveServices.Search.NewsSearch.Models;

namespace ExemploBingSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebSearchClient(new Microsoft.Azure.CognitiveServices.Search.WebSearch.ApiKeyServiceClientCredentials(""));
            var results = client.Web.SearchAsync(query: "receita de bolo de laranja").Result;

            var newsClient = new NewsSearchClient(new Microsoft.Azure.CognitiveServices.Search.NewsSearch.ApiKeyServiceClientCredentials(""));
            var news = newsClient.News.SearchAsync(query: "game of thrones").Result;

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(results));
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(news));
            Console.ReadLine();
        }
    }
}
