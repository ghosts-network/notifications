using System;
using System.IO;
using GhostNetwork.Notifications;
using GhostNetwork.Notifications.Channels.Smtp;
using GhostNetwork.Notifications.Channels.Smtp.DependencyInjection;
using GhostNetwork.Notifications.Channels.Web.DependencyInjection;
using GhostNetwork.Notifications.Core;
using GhostNetwork.Notifications.Core.DependencyInjection;
using GhostNetwork.Notifications.TemplateCompilers.Handlebars;
using GhostNetwork.Notifications.TemplateStorages.Folder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddNotification(channels =>
    {
        channels
            .AddSmtpChannel(configuration =>
            {
                var smtpOverSsl = builder.Configuration.GetValue("SMTP_SSL_ENABLED", true);
                configuration.Smtp = new SmtpConfiguration
                {
                    EnableSsl = smtpOverSsl,
                    Host = builder.Configuration["SMTP_HOST"],
                    Port = builder.Configuration.GetValue("SMTP_PORT", smtpOverSsl ? 465 : 25),
                    UserName = builder.Configuration["SMTP_USERNAME"],
                    Password = builder.Configuration["SMTP_PASSWORD"]
                };

                configuration.WorkersCount = 3;

                configuration.Sender = new SenderInfo
                {
                    DisplayName = "GhostNetwork",
                    Email = builder.Configuration["SMTP_USERNAME"]
                };
            })
            .AddWebChannel();
    })
    .AddEventStorage<EventTypesStorage>()
    .AddTemplateCompiler<HandlebarsTemplateCompiler>()
    .AddFolderTemplateStorage(new FolderTemplateStorageOptions(Path.Combine(Environment.CurrentDirectory, "templates")));

var app = builder.Build();

app
    .MapPost("events/{id}/trigger", async (
        [FromRoute] string id,
        [FromBody] Trigger trigger,
        [FromServices] NotificationManager notificationManager) =>
    {
        await notificationManager.TriggerAsync(id, trigger);
        return Results.NoContent();
    });

app.Run();