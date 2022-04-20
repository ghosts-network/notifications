using System;

namespace GhostNetwork.Notifications.Core;

public record EventChannel(Guid ChannelId, string Template);
