using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Channels.Email;

public class EmailChannelTrigger : IChannelTrigger
{
    public static readonly Guid Id = new Guid("DCECBDB0-B876-4D51-A5E4-EB80F9A93E7F");
    public Channel Channel { get; } = new Channel(Id, "Email");

    public void FireAndForget(string message, Recipient recipient)
    {
        Console.WriteLine($"Email notification to {recipient.Id}:{recipient.Email}. Message: {message}");
    }
}
