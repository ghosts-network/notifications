using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public interface IChannelTrigger
{
    Task FireAndForgetAsync(CompiledContent message, Recipient recipient);
}