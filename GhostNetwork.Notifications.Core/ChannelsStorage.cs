using System.Collections.Generic;

namespace GhostNetwork.Notifications.Core;

public class ChannelsStorage
{
    private readonly Dictionary<string, IChannelTrigger> triggers;

    public ChannelsStorage()
    {
        triggers = new Dictionary<string, IChannelTrigger>();
    }

    public void RegisterTrigger(string channelId, IChannelTrigger trigger)
    {
        triggers[channelId] = trigger;
    }

    public IChannelTrigger GetTrigger(string channelId)
    {
        return triggers[channelId];
    }

    public bool HasTriggerForChannel(string channelId)
    {
        return triggers.ContainsKey(channelId);
    }
}
