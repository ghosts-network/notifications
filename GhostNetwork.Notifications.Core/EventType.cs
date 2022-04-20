using System;
using System.Collections.Generic;

namespace GhostNetwork.Notifications.Core;

public record EventType(Guid Id, string Name, IEnumerable<EventChannel> Channels);
