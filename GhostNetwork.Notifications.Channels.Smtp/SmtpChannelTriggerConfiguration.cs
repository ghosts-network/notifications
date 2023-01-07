namespace GhostNetwork.Notifications.Channels.Smtp;

public class SmtpChannelTriggerConfiguration
{
    public SenderInfo Sender { get; set; }
    
    public SmtpConfiguration Smtp { get; set; }

    public int WorkersCount { get; set; } = 1;
}