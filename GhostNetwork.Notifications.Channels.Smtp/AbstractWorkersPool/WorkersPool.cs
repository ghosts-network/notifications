using System.Threading.Channels;

namespace GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;

public class WorkersPool<TWorkPayload> : IAsyncDisposable
{
    private readonly List<PoolWorker<TWorkPayload>> workers;
    private readonly Channel<TWorkPayload> channel;

    public WorkersPool(
        IWorkerFactory<TWorkPayload> workerFactory,
        int poolSize)
    {
        if (poolSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(poolSize), "Pool should have at least 1 worker");
        }
        
        channel = Channel.CreateBounded<TWorkPayload>(poolSize);

        workers = Enumerable.Range(0, poolSize)
            .Select(_ =>
            {
                var worker = workerFactory.Create(channel.Reader);
                worker.StartAsync();

                return worker;
            })
            .ToList();
    }

    public async Task SendAsync(TWorkPayload message,
        CancellationToken cancellationToken = default)
    {
        await channel.Writer.WriteAsync(message, cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var worker in workers)
        {
            await worker.DisposeAsync();
        }
    }
}