using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public interface IUserSettingsStorage
{
    Task<UserSettings> GetUserSettingsAsync(string id);
}
