using System.Text.Json;

namespace GhostNetwork.Notifications.Core;

public interface ITemplateCompiler
{
    string GetMessage(Template template, JsonElement body);
}