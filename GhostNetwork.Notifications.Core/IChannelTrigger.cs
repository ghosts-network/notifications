namespace GhostNetwork.Notifications.Core;

public interface IChannelTrigger
{
    Channel Channel { get; }
    void FireAndForget(string message, Recipient recipient);
}