using System;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Channels.Web;

public class WebChannelTrigger : IChannelTrigger
{
    public Task FireAndForgetAsync(CompiledContent message, Recipient recipient)
    {
        Console.WriteLine($"Web notification to {recipient.Id}:{recipient.Email}. Message: {message}");
        return Task.CompletedTask;
    }
}
