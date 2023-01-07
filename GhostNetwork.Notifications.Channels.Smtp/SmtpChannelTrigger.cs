using GhostNetwork.Notifications.Core;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace GhostNetwork.Notifications.Channels.Smtp;

public class SmtpChannelTrigger : IChannelTrigger
{
    private readonly SmtpSender sender;
    private readonly IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration;

    public const string Id = "email";
    public Channel Channel { get; } = new Channel(Id, "Email");

    public SmtpChannelTrigger(
        SmtpSender sender,
        IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration)
    {
        this.sender = sender;
        this.configuration = configuration;
    }

    public void FireAndForget(CompiledContent template, Recipient recipient)
    {
        var senderInfo = configuration.CurrentValue.Sender;
        
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(senderInfo.DisplayName, senderInfo.Email));
        emailMessage.To.Add(new MailboxAddress(recipient.FullName, recipient.Email));
        emailMessage.Subject = template.Subject;
        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            ContentTransferEncoding = ContentEncoding.Default,
            Text = template.Main
        };

        sender.SendAsync(emailMessage)
            .GetAwaiter()
            .GetResult();
    }
}
