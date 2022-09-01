using System.Text.Json;

namespace GhostNetwork.Notifications.Core;

public interface ITemplateCompiler
{
    string GetMessage(string eventType, string channel, JsonElement body);
}