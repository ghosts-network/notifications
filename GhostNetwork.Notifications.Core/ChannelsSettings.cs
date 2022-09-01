using System;
using System.Collections.Generic;

namespace GhostNetwork.Notifications.Core;

public class ChannelsSettings
{
    private readonly UserChannelSettings defaultValue = new UserChannelSettings(true);
    private readonly Dictionary<string, UserChannelSettings> settings;

    public ChannelsSettings(Dictionary<string, UserChannelSettings> settings)
    {
        this.settings = settings;
    }

    public UserChannelSettings this[string channelId] => settings.ContainsKey(channelId)
        ? settings[channelId]
        : defaultValue;
}