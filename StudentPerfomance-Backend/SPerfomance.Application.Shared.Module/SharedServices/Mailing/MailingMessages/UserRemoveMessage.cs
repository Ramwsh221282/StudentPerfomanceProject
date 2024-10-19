namespace SPerfomance.Application.Shared.Module.SharedServices.Mailing.MailingMessages;

public sealed class UserRemoveMessage : MailingMessage
{
	public UserRemoveMessage(string destination, string message = "Ваша учетная запись была удалена с платформы SPerfomance.") : base(destination, message)
	{
		Subject = "SPerfomance. Удаление учетной записи";
	}
}
