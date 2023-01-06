using GhostNetwork.Notifications.TemplateStorages.Folder;
using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Core.DependencyInjection;

public static class NotificationBuilderExtensions
{
    public static NotificationBuilder AddFolderTemplateStorage(this NotificationBuilder builder, FolderTemplateStorageOptions options)
    {
        builder.Services.AddSingleton(options);
        builder.Services.AddScoped<ITemplateStorage, FolderTemplateStorage>();
        builder.Services.AddSingleton<IFileSelector, DefaultFileSelector>();

        return builder;
    }
}
