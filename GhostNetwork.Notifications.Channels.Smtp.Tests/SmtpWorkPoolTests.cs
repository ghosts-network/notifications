using GhostNetwork.Notifications.Channels.Smtp.AbstractWorkersPool;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Moq;
using NUnit.Framework;

namespace GhostNetwork.Notifications.Channels.Smtp.Tests;

[TestFixture]
public class SmtpWorkPoolTests
{
    [TestCase("", "")]
    public async Task Test(string userName, string password)
    {
        // Arrange
        const int poolSize = 3;
        const int messageCount = 6;

        var smtpConfiguration = new SmtpChannelTriggerConfiguration
        {
            Smtp = new SmtpConfiguration
            {
                Host = "smtp.gmail.com",
                Port = 465,
                EnableSsl = true,
                UserName = userName,
                Password = password
            }
        };

        var configurationMock = Mock.Of<IOptionsMonitor<SmtpChannelTriggerConfiguration>>(c => c.CurrentValue == smtpConfiguration);

        var factory = new SmtpWorkerFactory(configurationMock);
        var pool = new WorkersPool<MimeMessage>(factory, poolSize);

        // Act
        var tasks = Enumerable.Range(0, messageCount)
            .Select(i =>
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(userName, userName));
                emailMessage.To.Add(new MailboxAddress(smtpConfiguration.Smtp.UserName, "asdhgbotnbdfsdfhjkh@yopmail.com"));
                emailMessage.Subject = $"Test subject {i}";
                emailMessage.Body = new TextPart(TextFormat.Html)
                {
                    ContentTransferEncoding = ContentEncoding.Default,
                    Text = $"Test body {i}"
                };
                
                return pool.SendAsync(emailMessage);
            })
            .ToList();

        await Task.WhenAll(tasks);
        Thread.Sleep(10 * 1000);
    }
}