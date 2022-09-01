using System.Text.Json;
using System.Text.Json.Nodes;

namespace GhostNetwork.Notifications.Core;

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