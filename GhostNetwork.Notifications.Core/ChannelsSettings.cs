using System;
using System.Collections.Generic;

namespace GhostNetwork.Notifications.Core;

public class ChannelsSettings
{
    private readonly UserChannelSettings defaultValue = new UserChannelSettings(true);
    private readonly Dictionary<Guid, UserChannelSettings> settings;

    public ChannelsSettings(Dictionary<Guid, UserChannelSettings> settings)
    {
        this.settings = settings;
    }

    public UserChannelSettings this[Guid channelId] => settings.ContainsKey(channelId)
        ? settings[channelId]
        : defaultValue;
}