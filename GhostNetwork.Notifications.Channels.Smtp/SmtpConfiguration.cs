namespace GhostNetwork.Notifications.Channels.Smtp;

public class SmtpConfiguration
{
    public string Host { get; set; }

    public int Port { get; set; } = 25;

    public bool EnableSsl { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }
}
