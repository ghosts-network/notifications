namespace GhostNetwork.Notifications.Core;

public interface IChannelTrigger
{
    Channel Channel { get; }
    void FireAndForget(CompiledTemplate message, Recipient recipient);
}