using Microsoft.AspNetCore;
using Noa.ContactNotifier.Docker;
using Noa.ContactNotifier.Docker.Configuration;
using System;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var webHost = BuildWebHost(args);
        await webHost.RunAsync().ConfigureAwait(false);

        // var healthHost = BuildHealthWebHost(args);
        // await healthHost.RunAsync().ConfigureAwait(false);
    }

    private static IWebHost BuildWebHost(string[] args) => BuildGenericWebHost<Startup>(args);

    public static IWebHost BuildGenericWebHost<TStartup>(string[] args) where TStartup : class
    {
        var configurationRoot = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false).AddEnvironmentVariables().Build();
        var configuration = configurationRoot.GetSection(ServiceConfiguration.ServiceName).Get<ServiceConfiguration>();

        return WebHost.CreateDefaultBuilder(args)
          .UseConfiguration(configurationRoot)
          .UseStartup<TStartup>()
          .UseUrls($"http://+:{configuration.Api.Port}")
          .Build();
    }
}
