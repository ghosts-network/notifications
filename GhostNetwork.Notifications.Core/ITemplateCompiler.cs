using System.Text.Json;

namespace GhostNetwork.Notifications.Core;

public interface ITemplateCompiler
{
    CompiledTemplate GetMessage(Template template, JsonElement body);
}