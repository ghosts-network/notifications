using System.Text.Json;
using GhostNetwork.Notifications.Core;
using HandlebarsDotNet;
using HandlebarsDotNet.Extension.Json;

namespace GhostNetwork.Notifications.Api.Controllers;

public class TemplateCompiler : ITemplateCompiler
{
    public CompiledTemplate GetMessage(Template template, JsonElement body)
    {
        var hb = Handlebars.Create()!;
        hb.Configuration.UseJson();
        var result = hb.Compile(template.Main).Invoke(body);

        return new CompiledTemplate(result);
    }
}