using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public class NotificationManager
{
    private readonly IEventTypesStorage eventTypesStorage;
    private readonly IUserSettingsStorage userSettingsStorage;
    private readonly ITemplateCompiler templateCompiler;
    private readonly ITemplateStorage templateStorage;
    private readonly ChannelsStorage channelTriggerProvider;

    public NotificationManager(
        IEventTypesStorage eventTypesStorage,
        IUserSettingsStorage userSettingsStorage,
        ITemplateCompiler templateCompiler,
        ITemplateStorage templateStorage,
        ChannelsStorage channelTriggerProvider)
    {
        this.eventTypesStorage = eventTypesStorage;
        this.userSettingsStorage = userSettingsStorage;
        this.templateCompiler = templateCompiler;
        this.templateStorage = templateStorage;
        this.channelTriggerProvider = channelTriggerProvider;
    }

    public async Task TriggerAsync(string eventTypeId, Trigger trigger)
    {
        var eventType = await eventTypesStorage.GetByIdAsync(eventTypeId);
        if (eventType == null)
        {
            throw new Exception();
        }

        foreach (var eventChannel in eventType.Channels)
        {
            if (!channelTriggerProvider.HasTriggerForChannel(eventChannel.ChannelId))
            {
                throw new Exception();
            }
        }

        var tasks = new List<Task>(trigger.Recipients.Count() * eventType.Channels.Count());
        foreach (var recipient in trigger.Recipients)
        {
            foreach (var eventChannel in eventType.Channels)
            {
                var userSettings = await userSettingsStorage.GetUserSettingsAsync(recipient.Id);
                if (!userSettings[eventType.Id][eventChannel.ChannelId].Enabled)
                {
                    continue;
                }

                var bodyWithRecipient = trigger.Object.AddRecipient(recipient);
                var main = templateCompiler.GetMessage(
                    await templateStorage.GetTemplateAsync(new TemplateSelector(eventType.Id, eventChannel.ChannelId)),
                    bodyWithRecipient);
                var subject = templateCompiler.GetMessage(
                    await templateStorage.GetTemplateAsync(new TemplateSelector(eventType.Id, eventChannel.ChannelId, "subject")),
                    bodyWithRecipient);

                var task = channelTriggerProvider
                    .GetTrigger(eventChannel.ChannelId)
                    .FireAndForgetAsync(new CompiledContent(main, subject), recipient);

                tasks.Add(task);
            }
        }

        await Task.WhenAll(tasks);
    }
}