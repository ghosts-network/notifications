using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Core.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static NotificationBuilder AddNotification(this IServiceCollection serviceCollection,
        Action<ChannelsOptions> action)
    {
        var options = new ChannelsOptions(serviceCollection);
        action(options);

        serviceCollection.AddSingleton<ChannelsStorage>(provider =>
        {
            var storage = new ChannelsStorage();
            foreach (var triggerType in options.TriggerTypes)
            {
                storage.RegisterTrigger((provider.GetRequiredService(triggerType) as IChannelTrigger)!);
            }

            return storage;
        });

        serviceCollection.AddSingleton<IUserSettingsStorage, NullUserSettingsStorage>();

        serviceCollection.AddScoped<NotificationManager>();

        return new NotificationBuilder(serviceCollection);
    }

    private class NullUserSettingsStorage : IUserSettingsStorage
    {
        public Task<UserSettings> GetUserSettingsAsync(string id)
        {
            return Task.FromResult(new UserSettings(new Dictionary<string, Dictionary<string, UserChannelSettings>>()));
        }
    }
}