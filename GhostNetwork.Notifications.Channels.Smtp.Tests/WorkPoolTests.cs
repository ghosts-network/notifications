using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;
using NUnit.Framework;

namespace GhostNetwork.Notifications.Channels.Smtp.Tests;

[TestFixture]
public class WorkPoolTests
{
    [Test]
    public async Task Test()
    {
        // Arrange
        const int poolSize = 3;
        const int messageCount = 6;
        var output = new ConcurrentQueue<string>();
        var factory = new TestWorkerFactory(output);
        var pool = new WorkersPool<TestPayload>(factory, poolSize);

        // Act
        var tasks = Enumerable.Range(0, messageCount)
            .Select(i => pool.SendAsync(new TestPayload(i.ToString())))
            .ToList();

        await Task.WhenAll(tasks);

        // Assert
        Assert.AreEqual(output.Count, messageCount);
    }

    private record TestPayload(string Result);
    
    private class TestPoolWorker : PoolWorker<TestPayload>
    {
        private readonly ConcurrentQueue<string> output;

        public TestPoolWorker(ConcurrentQueue<string> output, ChannelReader<TestPayload> channelReader) : base(channelReader)
        {
            this.output = output;
        }

        protected override Task ProcessAsync(TestPayload payload)
        {
            output.Enqueue(payload.Result);
            return Task.CompletedTask;
        }

        public override ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }

    private class TestWorkerFactory : IWorkerFactory<TestPayload>
    {
        private readonly ConcurrentQueue<string> output;

        public TestWorkerFactory(ConcurrentQueue<string> output)
        {
            this.output = output;
        }
        
        public PoolWorker<TestPayload> Create(ChannelReader<TestPayload> channelReader)
        {
            return new TestPoolWorker(output, channelReader);
        }
    }
}