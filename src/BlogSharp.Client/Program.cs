using BlogSharp.Client.Services;
using BlogSharp.Shared.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

// Register HttpClient to point back to the Server project
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Register the Client implementation of the interface
builder.Services.AddScoped<IBlogPostService, ClientBlogPostService>();

await builder.Build().RunAsync();