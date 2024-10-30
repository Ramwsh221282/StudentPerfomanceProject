using SPerfomance.Application.Services.Mailing.MailingMessages;

namespace SPerfomance.Application.Services.Mailing;

public interface IMailingService
{
	Task SendMessage(MailingMessage message);
}
