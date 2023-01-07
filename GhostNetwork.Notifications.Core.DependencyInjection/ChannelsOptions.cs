using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Core.DependencyInjection;

public class ChannelsOptions
{
    private readonly List<ChannelConfig> channels = new List<ChannelConfig>();
    public IEnumerable<ChannelConfig> Channels => channels;

    public ChannelsOptions(IServiceCollection services)
    {
        Services = services;
    }

    public void AddTrigger<TTrigger>(string id)
        where TTrigger : class, IChannelTrigger
    {
        channels.Add(new ChannelConfig(id, typeof(TTrigger)));
    }

    public IServiceCollection Services { get; }

    public record ChannelConfig(string Id, Type TriggerType);
}