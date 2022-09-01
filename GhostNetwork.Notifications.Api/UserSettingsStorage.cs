using System.Collections.Generic;
using System.Linq;
using GhostNetwork.Notifications.Channels.Email;
using GhostNetwork.Notifications.Channels.Web;
using GhostNetwork.Notifications.Core;

namespace NotificationsHub.Api.Controllers;

public class UserSettingsStorage
{
    private readonly List<UserSettingsPair> userSettings = new List<UserSettingsPair>
    {
        new UserSettingsPair("0E41DA93-1499-4ED4-AEA0-767B5A7F2DAA", new UserSettings(
            new Dictionary<string, Dictionary<string, UserChannelSettings>>
            {
                ["E125F603-5746-4BB5-B2AE-07B6C388B2F7"] = new Dictionary<string, UserChannelSettings>
                {
                    [WebChannelTrigger.Id] = new UserChannelSettings(false),
                    [EmailChannelTrigger.Id] = new UserChannelSettings(true)
                }
            }))
    };

    public UserSettings GetUserSettings(string id)
    {
        return userSettings.FirstOrDefault(x => x.UserId == id)?.UserSettings ?? new UserSettings(new Dictionary<string, Dictionary<string, UserChannelSettings>>());
    }

    private record UserSettingsPair(string UserId, UserSettings UserSettings);
}