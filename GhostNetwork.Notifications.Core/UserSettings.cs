using System.Collections.Generic;
using System.Linq;

namespace GhostNetwork.Notifications.Core;

public class UserSettings
{
    private readonly ChannelsSettings defaultSettings = new ChannelsSettings(new Dictionary<string, UserChannelSettings>());
    private readonly Dictionary<string, ChannelsSettings> settings;

    public UserSettings(Dictionary<string, Dictionary<string, UserChannelSettings>> settings)
    {
        this.settings = settings.ToDictionary(s => s.Key,
            s => new ChannelsSettings(s.Value));
    }

    public ChannelsSettings this[string eventTypeId] => settings.ContainsKey(eventTypeId)
        ? settings[eventTypeId]
        : defaultSettings;
}
