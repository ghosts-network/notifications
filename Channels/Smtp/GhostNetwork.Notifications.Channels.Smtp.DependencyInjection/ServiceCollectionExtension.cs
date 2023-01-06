using System;
using GhostNetwork.Notifications.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Channels.Smtp.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ChannelsOptions AddSmtpChannel(this ChannelsOptions options,
        Action<SmtpChannelTriggerConfiguration> configureOptions)
    {
        options.Services.Configure(configureOptions);

        options.Services.AddSingleton<SmtpWorkerFactory>();
        options.Services.AddSingleton<SmtpSender>();
        options.Services.AddSingleton<SmtpChannelTrigger>();
        
        options.AddTrigger<SmtpChannelTrigger>();

        return options;
    }
}
