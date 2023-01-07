using System.Threading.Tasks;
using GhostNetwork.Notifications.Core;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GhostNetwork.Notifications.Channels.SendGrid;

public class SendGridChannelTrigger : IChannelTrigger
{
    private readonly SendGridClient client;
    private readonly IOptionsMonitor<SendGridChannelTriggerConfiguration> configuration;

    public SendGridChannelTrigger(
        SendGridClient client,
        IOptionsMonitor<SendGridChannelTriggerConfiguration> configuration)
    {
        this.client = client;
        this.configuration = configuration;
    }

    public async Task FireAndForgetAsync(CompiledContent message, Recipient recipient)
    {
        var config = configuration.CurrentValue;
        
        var from = new EmailAddress(config.Sender.Email, config.Sender.DisplayName);
        var to = new EmailAddress(recipient.Email, recipient.Email);
        var msg = MailHelper.CreateSingleEmail(from, to, message.Subject, message.Main, message.Main);

        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}