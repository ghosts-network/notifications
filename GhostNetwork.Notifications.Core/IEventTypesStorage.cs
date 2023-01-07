using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public interface IEventTypesStorage
{
    Task<EventType?> GetByIdAsync(string id);
}