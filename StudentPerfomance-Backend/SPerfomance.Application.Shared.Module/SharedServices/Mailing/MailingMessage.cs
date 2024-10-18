using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.Shared.Module.SharedServices.Mailing;

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
