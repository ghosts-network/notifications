using System;
using System.Linq;
using GhostNetwork.Notifications.Core;
using Microsoft.AspNetCore.Mvc;
using NotificationsHub.Api.Controllers;

namespace GhostNetwork.Notifications.Api.Controllers;

[ApiController]
[Route("event-types")]
public class EventTypesController : ControllerBase
{
    private readonly EventTypesStorage eventTypesStorage;
    private readonly UserSettingsStorage userSettingsStorage;
    private readonly TemplateCompiler templateCompiler;
    private readonly ChannelsStorage channelTriggerProvider;

    public EventTypesController(EventTypesStorage eventTypesStorage,
        UserSettingsStorage userSettingsStorage,
        TemplateCompiler templateCompiler,
        ChannelsStorage channelTriggerProvider)
    {
        this.eventTypesStorage = eventTypesStorage;
        this.userSettingsStorage = userSettingsStorage;
        this.templateCompiler = templateCompiler;
        this.channelTriggerProvider = channelTriggerProvider;
    }

    [HttpGet]
    public ActionResult Get()
    {
        return Ok(eventTypesStorage.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult Get([FromRoute] Guid id)
    {
        return Ok(eventTypesStorage.GetById(id));
    }

    [HttpPost("{id}/trigger")]
    public ActionResult Trigger([FromRoute] Guid id, [FromBody] Trigger trigger)
    {
        var eventType = eventTypesStorage.GetById(id);
        if (eventType == null)
        {
            ModelState.AddModelError("id", "Event type not found");
            return BadRequest();
        }

        foreach (var eventChannel in eventType.Channels)
        {
            if (!channelTriggerProvider.HasTriggerForChannel(eventChannel.ChannelId))
            {
                ModelState.AddModelError("ChannelId", "Channel not found");
                BadRequest();
            }
        }

        var templates = eventType.Channels
            .Select(eventChannel => new
            {
                Template = templateCompiler.GetTemplate(eventChannel.Template),
                eventChannel.ChannelId
            })
            .ToList();

        foreach (var recipient in trigger.Recipients)
        {
            foreach (var template in templates)
            {
                var userSettings = userSettingsStorage.GetUserSettings(recipient.Id);
                if (!userSettings[id][template.ChannelId].Enabled)
                {
                    continue;
                }

                var message = template.Template.Invoke(trigger.Object.AddRecipient(recipient))!;
                channelTriggerProvider.GetTrigger(template.ChannelId).FireAndForget(message, recipient);
            }
        }

        return NoContent();
    }
}