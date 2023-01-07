using System;
using System.IO;
using System.Threading.Tasks;
using GhostNetwork.Notifications.Core;
using NUnit.Framework;

namespace GhostNetwork.Notifications.TemplateStorages.Folder.Tests;

[TestFixture]
public class FolderTemplateStorageTests
{
    private static readonly string TemplatesDirectory = Path.Combine(Environment.CurrentDirectory, "templates");

    [TestCase("web")]
    [TestCase("email")]
    public async Task GetTemplateAsync_ReturnsDefaultTemplate(string channelId)
    {
        // Arrange
        var fileSelector = new DefaultFileSelector();
        var storage = new FolderTemplateStorage(fileSelector, new FolderTemplateStorageOptions(TemplatesDirectory));

        var templateSelector = new TemplateSelector("user-email-changed", channelId);

        // Act
        var result = await storage.GetTemplateAsync(templateSelector);

        // Assert
        Assert.AreEqual($"<p>{channelId} user-email-changed template</p>", result);
    }

    [Test]
    public async Task GetTemplateAsync_WithTemplateTypeSubject_ReturnsSubjectTemplate()
    {
        // Arrange
        var fileSelector = new DefaultFileSelector();
        var storage = new FolderTemplateStorage(fileSelector, new FolderTemplateStorageOptions(TemplatesDirectory));

        var templateSelector = new TemplateSelector("user-email-changed", "email", "subject");

        // Act
        var result = await storage.GetTemplateAsync(templateSelector);

        // Assert
        Assert.AreEqual("<p>email user-email-changed subject template</p>", result);
    }
}