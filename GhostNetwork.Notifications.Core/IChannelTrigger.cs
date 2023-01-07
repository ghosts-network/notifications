namespace GhostNetwork.Notifications.Core;

public interface IChannelTrigger
{
    Channel Channel { get; }
    void FireAndForget(CompiledContent message, Recipient recipient);
}