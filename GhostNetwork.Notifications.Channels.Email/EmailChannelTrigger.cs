using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Channels.Email;

public class EmailChannelTrigger : IChannelTrigger
{
    public const string Id = "email";
    public Channel Channel { get; } = new Channel(Id, "Email");

    public void FireAndForget(string message, Recipient recipient)
    {
        Console.WriteLine($"Email notification to {recipient.Id}:{recipient.Email}. Message: {message}");
    }
}
