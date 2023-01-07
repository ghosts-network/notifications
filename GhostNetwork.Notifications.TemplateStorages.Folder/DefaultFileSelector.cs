using System.Collections.Generic;
using System.IO;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.TemplateStorages.Folder;

public class DefaultFileSelector : IFileSelector
{
    private readonly string extension;

    public DefaultFileSelector(string extension = "html")
    {
        this.extension = extension;
    }

    public string BuildFilePath(TemplateSelector selector)
    {
        var fileNameParts = new List<string>(4)
        {
            selector.ChannelId
        };

        if (selector.TemplateType != null)
        {
            fileNameParts.Add(selector.TemplateType);
        }

        if (selector.Culture != null)
        {
            fileNameParts.Add(selector.Culture);
        }

        fileNameParts.Add(extension);

        return Path.Combine(selector.EventTypeId, string.Join('.', fileNameParts));
    }
}