using Azure.AI.TextAnalytics;
using System.Threading.Tasks;

namespace BlazorSignalRChat.Client.TextService
{
    public class TextAnalyticsService
    {
        private readonly TextAnalyticsClient _client;

        public TextAnalyticsService(TextAnalyticsClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> AnalyzeSentimentAsync(string text)
        {
            DocumentSentiment documentSentiment = await _client.AnalyzeSentimentAsync(text);
            return documentSentiment.Sentiment.ToString();
        }
    }
}
