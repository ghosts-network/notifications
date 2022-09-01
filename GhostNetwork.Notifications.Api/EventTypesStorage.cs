using System.Collections.Generic;
using System.Linq;
using GhostNetwork.Notifications.Channels.Email;
using GhostNetwork.Notifications.Channels.Web;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications;

public class EventTypesStorage
{
    private readonly List<EventType> eventTypes = new List<EventType>
    {
        new EventType("E125F603-5746-4BB5-B2AE-07B6C388B2F7",
            "Time off has been requested",
            new []
            {
                new EventChannel(EmailChannelTrigger.Id),// "Email: {{recipient.fullName}} Time off request from {{requester.fullName}} is pending your approval"),
                new EventChannel(WebChannelTrigger.Id)//, "Web: Time off request from {{requester.fullName}} is pending your approval")
            }),
        new EventType("5C51CC41-A6DF-4F93-BD28-768AD17DA416",
            "Time off has been discarded",
            new []
            {
                new EventChannel(EmailChannelTrigger.Id),//, "Time off request from {{requester.fullName}} has been discarded"),
                new EventChannel(WebChannelTrigger.Id)//, "Time off request from {{requester.fullName}} has been discarded")
            })
    };
    
    public IEnumerable<EventType> GetAll()
    {
        return eventTypes;
    }
    
    public EventType? GetById(string id)
    {
        return eventTypes.FirstOrDefault(x => x.Id == id);
    }
}