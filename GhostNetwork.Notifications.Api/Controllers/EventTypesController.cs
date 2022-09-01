using System.Threading.Tasks;
using GhostNetwork.Notifications.Core;
using Microsoft.AspNetCore.Mvc;

namespace GhostNetwork.Notifications.Api.Controllers;

[ApiController]
[Route("event-types")]
public class EventTypesController : ControllerBase
{
    private readonly EventTypesStorage eventTypesStorage;
    private readonly NotificationManager notificationManager;

    public EventTypesController(
        EventTypesStorage eventTypesStorage,
        NotificationManager notificationManager)
    {
        this.eventTypesStorage = eventTypesStorage;
        this.notificationManager = notificationManager;
    }

    [HttpGet]
    public ActionResult Get()
    {
        return Ok(eventTypesStorage.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult Get([FromRoute] string id)
    {
        return Ok(eventTypesStorage.GetById(id));
    }

    [HttpPost("{id}/trigger")]
    public async Task<ActionResult> Trigger([FromRoute] string id, [FromBody] Trigger trigger)
    {
        await notificationManager.TriggerAsync(id, trigger);

        return NoContent();
    }
}