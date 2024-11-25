namespace SPerfomance.Application.Services.Mailing.MailingMessages;

public sealed class PasswordResetMailingMessage : MailingMessage
{
    public PasswordResetMailingMessage(string destination, string message)
        : base(destination, message)
    {
        Subject = "Новый пароль SPerfomance.";
    }
}
