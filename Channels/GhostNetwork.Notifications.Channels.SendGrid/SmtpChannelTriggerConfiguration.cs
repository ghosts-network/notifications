namespace GhostNetwork.Notifications.Channels.SendGrid;

public class SendGridChannelTriggerConfiguration
{
    public SenderInfo Sender { get; set; }
    
    public string ApiKey { get; set; }
}