using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Services.Mailing.MailingMessages;

public class MailingMessage
{
    public string Subject { get; init; } = string.Empty;
    public string Destination { get; init; }
    public string Message { get; init; }

    public MailingMessage(string destination, string message)
    {
        Destination = destination.FormatSpaces();
        Message = message.FormatSpaces();
    }
}
