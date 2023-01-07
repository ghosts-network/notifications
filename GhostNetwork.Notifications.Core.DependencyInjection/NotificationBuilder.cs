using Microsoft.Extensions.DependencyInjection;

namespace GhostNetwork.Notifications.Core.DependencyInjection;

public class NotificationBuilder
{
    public IServiceCollection Services { get; }

    public NotificationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public NotificationBuilder AddEventStorage<TEventStorage>()
        where TEventStorage : class, IEventTypesStorage
    {
        Services.AddScoped<IEventTypesStorage, TEventStorage>();
        
        return this;
    }

    public NotificationBuilder AddTemplateCompiler<TTemplateCompiler>()
        where TTemplateCompiler : class, ITemplateCompiler
    {
        Services.AddScoped<ITemplateCompiler, TTemplateCompiler>();
        
        return this;
    }

    public NotificationBuilder AddTemplateStorage<TTemplateStorage>()
        where TTemplateStorage : class, ITemplateStorage
    {
        Services.AddScoped<ITemplateStorage, TTemplateStorage>();
        
        return this;
    }
}
