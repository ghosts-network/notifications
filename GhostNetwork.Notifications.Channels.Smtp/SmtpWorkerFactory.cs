using System.Threading.Channels;
using GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GhostNetwork.Notifications.Channels.Smtp;

public class SmtpWorkerFactory : IWorkerFactory<MimeMessage>
{
    private readonly IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration;

    public SmtpWorkerFactory(IOptionsMonitor<SmtpChannelTriggerConfiguration> configuration)
    {
        this.configuration = configuration;
    }
    
    public PoolWorker<MimeMessage> Create(ChannelReader<MimeMessage> channelReader)
    {
        return new SmtpPoolWorker(channelReader, configuration);
    }
}