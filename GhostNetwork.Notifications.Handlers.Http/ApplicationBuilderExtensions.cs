using GhostNetwork.Notifications.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GhostNetwork.Notifications.Handlers.Http;

public static class ApplicationBuilderExtensions
{
    public static IEndpointRouteBuilder UseNotifications(this IEndpointRouteBuilder app)
    {
        app
            .MapPost("events/{id}/trigger", async (
                [FromRoute] string id,
                [FromBody] Trigger trigger,
                [FromServices] NotificationManager notificationManager) =>
            {
                await notificationManager.TriggerAsync(id, trigger).ConfigureAwait(false);
                return Results.NoContent();
            });

        return app;
    }
}