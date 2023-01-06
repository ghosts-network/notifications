using GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GhostNetwork.Notifications.Channels.Smtp;

public class SmtpSender
{
    private readonly WorkersPool<MimeMessage> workersPool;

    public SmtpSender(SmtpWorkerFactory smtpWorkerFactory, IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration)
    {
        workersPool = new WorkersPool<MimeMessage>(smtpWorkerFactory, configuration.CurrentValue.WorkersCount);
    }

    public Task SendAsync(MimeMessage message, CancellationToken cancellationToken = default)
    {
        return workersPool.SendAsync(message, cancellationToken);
    }
}