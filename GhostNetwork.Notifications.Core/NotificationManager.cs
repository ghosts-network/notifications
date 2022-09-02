using System;
using System.Threading.Tasks;

namespace GhostNetwork.Notifications.Core;

public class NotificationManager
{
    private readonly IEventTypesStorage eventTypesStorage;
    private readonly IUserSettingsStorage userSettingsStorage;
    private readonly ITemplateCompiler templateCompiler;
    private readonly ITemplateStorage templateStorage;
    private readonly IChannelsStorage channelTriggerProvider;

    public NotificationManager(
        IEventTypesStorage eventTypesStorage,
        IUserSettingsStorage userSettingsStorage,
        ITemplateCompiler templateCompiler,
        ITemplateStorage templateStorage,
        IChannelsStorage channelTriggerProvider)
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

        foreach (var recipient in trigger.Recipients)
        {
            foreach (var eventChannel in eventType.Channels)
            {
                var userSettings = await userSettingsStorage.GetUserSettingsAsync(recipient.Id);
                if (!userSettings[eventType.Id][eventChannel.ChannelId].Enabled)
                {
                    continue;
                }

                var template = await templateStorage.GetTemplateAsync(new TemplateSelector(eventType.Id, eventChannel.ChannelId));
                var message = templateCompiler.GetMessage(template, trigger.Object.AddRecipient(recipient));

                channelTriggerProvider
                    .GetTrigger(eventChannel.ChannelId)
                    .FireAndForget(message, recipient);
            }
        }
    }
}