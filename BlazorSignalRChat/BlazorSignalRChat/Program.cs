using Azure.AI.TextAnalytics;
using Azure;
using BlazorSignalRChat.Client.Pages;
using BlazorSignalRChat.Client.TextService;
using BlazorSignalRChat.Components;
using BlazorSignalRChat.Hubs;
using Microsoft.Extensions.Azure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();


builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ConnectionString = builder.Configuration["AzureSignalR:ConnectionString"];   
});


string endpoint = builder.Configuration["AzureAiTextAnalytics:Endpoint"];
string apiKey = builder.Configuration["AzureAiTextAnalytics:ApiKey"];

var credentials = new AzureKeyCredential(apiKey);
var client = new TextAnalyticsClient(new Uri(endpoint), credentials);

builder.Services.AddSingleton(client).AddAntiforgery();
builder.Services.AddScoped<TextAnalyticsService, TextAnalyticsService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();

app.MapControllers();
app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapRazorComponents<App>()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(Counter).Assembly);


app.Run();
