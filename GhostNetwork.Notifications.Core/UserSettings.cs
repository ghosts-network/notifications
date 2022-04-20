using System;
using System.Collections.Generic;
using System.Linq;

namespace GhostNetwork.Notifications.Core;

public class UserSettings
{
    private readonly ChannelsSettings defaultSettings = new ChannelsSettings(new Dictionary<Guid, UserChannelSettings>());
    private readonly Dictionary<Guid, ChannelsSettings> settings;

    public UserSettings(Dictionary<Guid, Dictionary<Guid, UserChannelSettings>> settings)
    {
        this.settings = settings.ToDictionary(s => s.Key,
            s => new ChannelsSettings(s.Value));
    }

    public ChannelsSettings this[Guid eventTypeId] => settings.ContainsKey(eventTypeId)
        ? settings[eventTypeId]
        : defaultSettings;
}
