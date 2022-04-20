using System.Collections.Generic;
using System.Text.Json;

namespace GhostNetwork.Notifications.Core;

public record Trigger(JsonElement Object, IEnumerable<Recipient> Recipients);
