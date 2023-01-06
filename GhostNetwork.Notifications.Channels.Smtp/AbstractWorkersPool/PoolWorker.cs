using System.Threading.Channels;

namespace GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;

public abstract class PoolWorker<TWorkPayload> : IAsyncDisposable
{
    private readonly ChannelReader<TWorkPayload> channelReader;

    protected PoolWorker(ChannelReader<TWorkPayload> channelReader)
    {
        this.channelReader = channelReader;
    }
    
    public async Task StartAsync()
    {
        while (await channelReader.WaitToReadAsync())
        {
            while (channelReader.TryRead(out var payload))
            {
                await ProcessAsync(payload);
            }
        }
    }

    protected abstract Task ProcessAsync(TWorkPayload payload);
    public abstract ValueTask DisposeAsync();
}
