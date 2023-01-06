using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Core.DependencyInjection;

public class ChannelsOptions
{
    private readonly List<Type> triggerTypes = new List<Type>();
    public IEnumerable<Type> TriggerTypes => triggerTypes;

    public ChannelsOptions(IServiceCollection services)
    {
        Services = services;
    }

    public void AddTrigger<TTrigger>()
        where TTrigger : class, IChannelTrigger
    {
        triggerTypes.Add(typeof(TTrigger));
    }

    public IServiceCollection Services { get; }
}