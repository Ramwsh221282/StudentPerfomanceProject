namespace SPerfomance.Application.Services.Mailing.MailingMessages;

public sealed class PasswordRecoveryMailingMessage : MailingMessage
{
    public PasswordRecoveryMailingMessage(string destination, string message)
        : base(destination, message)
    {
        Subject = "Восстановление пароля SPerfomance";
    }
}
