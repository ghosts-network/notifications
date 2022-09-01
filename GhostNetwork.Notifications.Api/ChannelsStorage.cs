using System.Collections.Generic;
using System.Linq;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Api;

public class ChannelsStorage
{
    private readonly Dictionary<string, IChannelTrigger> triggers;

    public ChannelsStorage(IEnumerable<IChannelTrigger> triggers)
    {
        this.triggers = triggers.ToDictionary(trigger => trigger.Channel.Id);
    }
    
    public IChannelTrigger GetTrigger(string id)
    {
        return triggers[id];
    }

    public IEnumerable<Channel> GetChannels()
    {
        return triggers.Values.Select(trigger => trigger.Channel).ToList();
    }

    public bool HasTriggerForChannel(string id)
    {
        return triggers.ContainsKey(id);
    }
}
