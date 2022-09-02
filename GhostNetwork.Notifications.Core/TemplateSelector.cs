namespace GhostNetwork.Notifications.Core;

public record TemplateSelector(
    string EventTypeId,
    string ChannelId,
    string? TemplateType = null,
    string? Culture = null);