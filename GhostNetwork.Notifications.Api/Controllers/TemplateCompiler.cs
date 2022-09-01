using System.Text.Json;
using GhostNetwork.Notifications.Core;
using HandlebarsDotNet;
using HandlebarsDotNet.Extension.Json;

namespace GhostNetwork.Notifications.Api.Controllers;

public class TemplateCompiler : ITemplateCompiler
{
    public string GetMessage(string eventType, string channel, JsonElement body)
    {
        var hb = Handlebars.Create()!;
        hb.Configuration.UseJson();
        return hb.Compile("Email: {{recipient.fullName}} Time off request from {{requester.fullName}} is pending your approval").Invoke(body);
    }
}