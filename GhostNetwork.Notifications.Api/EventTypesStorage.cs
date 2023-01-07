using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Channels.Smtp;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications;

public class EventTypesStorage : IEventTypesStorage
{
    private readonly List<EventType> eventTypes = new List<EventType>
    {
        new EventType("email-confirmation",
            "Email confirmation after registration",
            new []
            {
                new EventChannel(SmtpChannelTrigger.Id)
            })
    };

    public Task<EventType?> GetByIdAsync(string id)
    {
        return Task.FromResult(eventTypes.FirstOrDefault(x => x.Id == id));
    }
}
