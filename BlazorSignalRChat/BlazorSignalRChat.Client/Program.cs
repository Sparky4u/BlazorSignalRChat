using BlazorSignalRChat.Client.TextService;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.EntityFrameworkCore;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var configuration = builder.Configuration;

builder.RootComponents.Add<HeadOutlet>("head::after");


string endpoint = configuration["AzureAiTextAnalytics:Endpoint"];
string apiKey = configuration["AzureAiTextAnalytics:ApiKey"];


if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
{
    throw new InvalidOperationException("Azure AI Text Analytics configuration values are missing. Please check appsettings.json.");
}


var credentials = new AzureKeyCredential(apiKey);
var textAnalyticsClient = new TextAnalyticsClient(new Uri(endpoint), credentials);


builder.Services.AddSingleton(textAnalyticsClient);
builder.Services.AddScoped<TextAnalyticsService>();


await builder.Build().RunAsync();
