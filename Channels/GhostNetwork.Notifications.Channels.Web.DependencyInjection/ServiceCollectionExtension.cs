using GhostNetwork.Notifications.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Channels.Web.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ChannelsOptions AddWebChannel(this ChannelsOptions options, string id)
    {
        options.Services.AddSingleton<WebChannelTrigger>();
        
        options.AddTrigger<WebChannelTrigger>(id);

        return options;
    }
}
