using System.Text;

namespace SPerfomance.Application.Services.Mailing.MailingMessages;

public class UserRegistrationMessage : MailingMessage
{
	public UserRegistrationMessage(string destination, string message) : base(destination, message)
	{
		Subject = "SPerfomance. Успешная регистрация";
		StringBuilder overrided = new StringBuilder();
		overrided.AppendLine("Вы были успешно зарегистированы на сервисе SPerfomance");
		overrided.AppendLine("Ваши данные для авторизации:");
		overrided.AppendLine(message);
		Message = overrided.ToString();
	}
}
