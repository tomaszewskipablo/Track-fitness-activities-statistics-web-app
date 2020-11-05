using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SportApp.Shared.Services;
using SportApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using SportApp.Client.LoginLogic;
using Blazored.LocalStorage;

namespace SportApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSingleton<LoginState>();

            builder.Services.AddScoped(sp => new HttpClient(
            sp.GetRequiredService<AuthorizationMessageHandler>()
            .ConfigureHandler(
            authorizedUrls: new[] { "" },
            scopes: new[] { "example.read", "example.write" }))
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ILoginServices, LocalLoginServices>();
            builder.Services.AddHttpClient("ServerAPI",
client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("ServerAPI"));


            await builder.Build().RunAsync();
        }
    }
}
