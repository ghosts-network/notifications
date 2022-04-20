using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Channels.Web;

public class WebChannelTrigger : IChannelTrigger
{
    public static readonly Guid Id = new Guid("B4256372-9BCE-4D78-90B0-FB13B687A224");
    public Channel Channel { get; } = new Channel(Id, "Web");

    public void FireAndForget(string message, Recipient recipient)
    {
        Console.WriteLine($"Web notification to {recipient.Id}:{recipient.Email}. Message: {message}");
    }
}
