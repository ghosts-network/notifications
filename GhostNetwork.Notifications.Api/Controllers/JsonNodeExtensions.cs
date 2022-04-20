using System.Text.Json;
using System.Text.Json.Nodes;
using GhostNetwork.Notifications.Core;

namespace GhostNetwork.Notifications.Api.Controllers;

public static class JsonNodeExtensions
{
    public static JsonElement AddRecipient(this JsonElement document, Recipient recipient)
    {
        var jsonObject = JsonObject.Create(document)!;
        var element = JsonSerializer.SerializeToNode(recipient, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        jsonObject.Add("recipient", element);

        return jsonObject.Deserialize<JsonElement>();
    }
}