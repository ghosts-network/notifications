using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.TemplateStorages.Folder;

public interface IFileSelector
{
    string BuildFilePath(TemplateSelector selector);
}
