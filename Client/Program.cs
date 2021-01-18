using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ImageRepository.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredModal();

            builder.Services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

            builder.Services.AddHttpClient("ImageRepository.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //add a second httpclient that can make unauthorized requests (https://chrissainty.com/avoiding-accesstokennotavailableexception-when-using-blazor-webassembly-hosted-template-with-individual-user-accounts/)
            builder.Services.AddHttpClient("ImageRepository.PublicServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ImageRepository.ServerAPI"));

            builder.Services.AddApiAuthorization();

            var host = builder.Build();

            host.Services.UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
