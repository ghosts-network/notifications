using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public interface ITemplateStorage
{
    Task<string> GetTemplateAsync(TemplateSelector selector);
}