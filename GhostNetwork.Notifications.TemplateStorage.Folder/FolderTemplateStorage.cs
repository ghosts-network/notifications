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
        var template = File.ReadAllText(Path.Combine(options.RootFolder, fileSelector.BuildFileName(selector)));
        return Task.FromResult(new Template(template));
    }
}

public interface IFileSelector
{
    string BuildFileName(TemplateSelector selector);
}

public class DefaultFileSelector : IFileSelector
{
    private readonly string extension;

    public DefaultFileSelector(string extension = "html")
    {
        this.extension = extension;
    }

    public string BuildFileName(TemplateSelector selector)
    {
        return selector.Culture == null
            ? $"{selector.ChannelId}.{extension}"
            : $"{selector.ChannelId}.{selector.Culture}.{extension}";
    }
}
