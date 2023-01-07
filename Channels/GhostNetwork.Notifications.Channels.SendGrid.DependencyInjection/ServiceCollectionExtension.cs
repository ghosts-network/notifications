using System;
using GhostNetwork.Notifications.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid;

namespace GhostNetwork.Notifications.Channels.SendGrid.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ChannelsOptions AddSendGridChannel(this ChannelsOptions options,
        string id,
        Action<SendGridChannelTriggerConfiguration> configureOptions)
    {
        options.Services.Configure(configureOptions);

        options.Services.AddTransient<SendGridClient>(provider =>
            new SendGridClient(provider
                .GetRequiredService<IOptions<SendGridChannelTriggerConfiguration>>().Value.ApiKey));
        options.Services.AddTransient<SendGridChannelTrigger>();

        options.AddTrigger<SendGridChannelTrigger>(id);

        return options;
    }
}