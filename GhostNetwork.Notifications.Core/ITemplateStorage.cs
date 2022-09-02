using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public interface ITemplateStorage
{
    Task<Template> GetTemplateAsync(TemplateSelector selector);
}