using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.TemplateStorage.Folder;

public class FolderTemplateStorage : ITemplateStorage
{
    private readonly IFileSelector fileSelector;
    private readonly FolderTemplateStorageOptions options;

    public FolderTemplateStorage(
        IFileSelector fileSelector,
        FolderTemplateStorageOptions options)
    {
        this.fileSelector = fileSelector;
        this.options = options;
    }
    
    public Task<Template> GetTemplateAsync(TemplateSelector selector)
    {
        var template = File.ReadAllText(Path.Combine(options.RootFolder, fileSelector.BuildFilePath(selector)));
        return Task.FromResult(new Template(template));
    }
}

public interface IFileSelector
{
    string BuildFilePath(TemplateSelector selector);
}

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
