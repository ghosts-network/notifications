using System;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Channels.Web;

public class WebChannelTrigger : IChannelTrigger
{
    public const string Id = "web";
    public Channel Channel { get; } = new Channel(Id, "Web");

    public void FireAndForget(CompiledContent message, Recipient recipient)
    {
        Console.WriteLine($"Web notification to {recipient.Id}:{recipient.Email}. Message: {message}");
    }
}
