using BlazorWasm;
using BlazorWasm.Interfaces;
using BlazorWasm.Services.Base;
using BlazorWasm.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app"); //the root component of our application (app.razor) will be loaded in a div with id="app"
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton(new HttpClient
{
    BaseAddress = new Uri("https://localhost:7149")
});

builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost:7149"));

builder.Services.AddScoped<IRoutineDataService, RoutineDataService>();

await builder.Build().RunAsync();
