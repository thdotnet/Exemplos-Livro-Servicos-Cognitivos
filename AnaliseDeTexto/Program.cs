using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;

namespace ExemploAnaliseDeTexto
{
    class Program
    {
        private const string SubscriptionKey = "xxxxxx";

        /// </summary>
        class ApiKeyServiceClientCredentials : ServiceClientCredentials
        {
            public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                return base.ProcessHttpRequestAsync(request, cancellationToken);
            }
        }

        static void Main(string[] args)
        {
            ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials())
            {
                Endpoint = "https://eastus2.api.cognitive.microsoft.com"
            };

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SentimentBatchResult resultSentiment = client.SentimentAsync
            (
                new MultiLanguageBatchInput
                (
                    new List<MultiLanguageInput>()
                    {
                        new MultiLanguageInput("en", "0", "I had the best day of my life."),
                        new MultiLanguageInput("pt", "1", "Hoje eu perdi duas horas do meu dia no trânsito caótico de São Paulo.")
                    }
                )
            ).Result;


            var resultLanguage = client.DetectLanguageAsync(new BatchInput
            {
                Documents = new List<Input>
                {
                    new Input(id: "3", text: "this is a sample text written in a foreign language")
                }
            }).Result;

            foreach(var document in resultLanguage.Documents)
            {
                foreach (var detectedLanguage in document.DetectedLanguages)
                {
                    Console.WriteLine(detectedLanguage.Name);
                }
            }

            var exemploTextoEmPt = "O filme da Capitã Marvel foi terrível para quem acompanha e lê as revistas em quadrinho";
            var resultKeyPhrases = client.KeyPhrasesAsync(new MultiLanguageBatchInput
            {
                Documents = new List<MultiLanguageInput>
                {
                    new MultiLanguageInput(language:"pt", id: "4", text: exemploTextoEmPt)
                }
            }).Result;

            foreach (var document in resultKeyPhrases.Documents)
            {
                foreach (var keyPhrase in document.KeyPhrases)
                {
                    Console.WriteLine(keyPhrase);
                }
            }

            // Printing sentiment results
            foreach (var document in resultSentiment.Documents)
            {
                Console.WriteLine($"Document ID: {document.Id} , Sentiment Score: {document.Score:0.00}");
            }

            Console.Read();
        }
    }
}
    