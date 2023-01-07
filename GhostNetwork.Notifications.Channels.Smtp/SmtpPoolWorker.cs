using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Polly;

namespace GhostNetwork.Notifications.Channels.Smtp;

public class SmtpPoolWorker : PoolWorker<MimeMessage>
{
    private const int RetryCount = 0;
    private readonly Func<int, TimeSpan> sleepDurationProvider = _ => TimeSpan.FromSeconds(10);

    private readonly IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration;
    private readonly SmtpClient client = new SmtpClient();

    public SmtpPoolWorker(
        ChannelReader<MimeMessage> channelReader,
        IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration) : base(channelReader)
    {
        this.configuration = configuration;
    }

    protected override async Task ProcessAsync(MimeMessage payload)
    {
        await SendAsync(payload);
    }

    private async Task SendAsync(
        MimeMessage message,
        CancellationToken cancellationToken = default)
    {
        await Policy
            .Handle<SocketException>()
            .Or<IOException>()
            .Or<SmtpCommandException>(exception => exception.StatusCode == SmtpStatusCode.MailboxBusy)
            .Or<SmtpCommandException>(exception => exception.StatusCode == SmtpStatusCode.MailboxUnavailable)
            .Or<SmtpCommandException>(exception => exception.StatusCode == SmtpStatusCode.ServiceNotAvailable)
            .WaitAndRetryAsync(RetryCount, sleepDurationProvider)
            .ExecuteAsync(async () =>
            {
                var config = configuration.CurrentValue.Smtp;
                if (!client.IsConnected)
                {
                    await client.ConnectAsync(config.Host, config.Port, config.EnableSsl, cancellationToken);
                }

                if (!client.IsAuthenticated)
                {
                    await client.AuthenticateAsync(config.UserName, config.Password, cancellationToken);
                }

                await client.SendAsync(message, cancellationToken);
            });
    }

    public override async ValueTask DisposeAsync()
    {
        await client.DisconnectAsync(true);
        client.Dispose();
    }
}