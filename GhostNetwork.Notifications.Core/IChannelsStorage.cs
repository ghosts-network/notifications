namespace GhostNetwork.Notifications.Core;

public interface IChannelsStorage
{
    IChannelTrigger GetTrigger(string channelId);
    bool HasTriggerForChannel(string channelId);
}