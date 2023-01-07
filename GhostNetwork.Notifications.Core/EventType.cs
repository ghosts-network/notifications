using System.Collections.Generic;

namespace GhostNetwork.Notifications.Core;

public record EventType(string Id, string Name, IEnumerable<EventChannel> Channels);
