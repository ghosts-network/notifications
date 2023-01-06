using System.IO;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.TemplateStorages.Folder;

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