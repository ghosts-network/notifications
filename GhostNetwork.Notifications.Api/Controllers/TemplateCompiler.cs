using HandlebarsDotNet;
using HandlebarsDotNet.Extension.Json;

namespace GhostNetwork.Notifications.Api.Controllers;

public class TemplateCompiler
{
    public HandlebarsTemplate<object, object> GetTemplate(string template)
    {
        var hb = Handlebars.Create()!;
        hb.Configuration.UseJson();
        return hb.Compile(template);
    }
}