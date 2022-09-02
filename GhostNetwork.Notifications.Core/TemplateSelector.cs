namespace GhostNetwork.Notifications.Core;

public record TemplateSelector(
    string EventTypeId,
    string ChannelId,
    string? Culture = null);