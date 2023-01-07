using System.Text.Json;
using GhostNetwork.Notifications.Core;
using HandlebarsDotNet.Extension.Json;

namespace GhostNetwork.Notifications.TemplateCompilers.Handlebars;

public class HandlebarsTemplateCompiler : ITemplateCompiler
{
    public string GetMessage(string template, JsonElement body)
    {
        var hb = HandlebarsDotNet.Handlebars.Create()!;
        hb.Configuration.UseJson();
        return hb.Compile(template).Invoke(body);
    }
}
