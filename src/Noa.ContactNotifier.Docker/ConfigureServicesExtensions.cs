using MongoDB.Driver;
using Noa.ContactNotifier.Docker.Configuration;
using Noa.ContactNotifier.Docker.Services;
using Noa.ContactNotifier.Repository.Repositories;
using Noa.ContactNotifier.Repository.Utils;
using Noa.PocketUi.Main.Services;

namespace Noa.ContactNotifier.Docker;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection ConfigureNoaServices(this IServiceCollection serviceCollection, ServiceConfiguration config) =>
        serviceCollection
            .AddSingleton(config)
            .AddSingleton(provider => MgdbDatabaseFactory.Create(config.Database.ConnectionString, config.Database.DatabaseName))
            .AddTransient<IContactRepository, MgdbContactRepository>()
            .AddTransient<IAuthenticationService, AuthenticationService>()
            .AddTransient<IContactService, ContactService>()
            .AddSingleton<INotificationService, NotificationService>()
            .AddSingleton<IMqttService, MqttService>(provider => new MqttService(provider.GetService<INotificationService>(), config));
}
