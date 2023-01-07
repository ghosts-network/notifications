using System.Threading.Channels;

namespace GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;

public interface IWorkerFactory<TWorkPayload>
{
    PoolWorker<TWorkPayload> Create(ChannelReader<TWorkPayload> channelReader);
}