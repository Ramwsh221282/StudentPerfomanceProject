using System.Net;
using System.Net.Mail;

using CSharpFunctionalExtensions;

namespace SPerfomance.Application.Shared.Module.SharedServices.Mailing;

public sealed class MailingService
{

	public async Task<Result> SendMessage(MailingMessage message)
	{
		using (SmtpClient sender = new SmtpClient(MailingSettings.SMTPServer, MailingSettings.SMTPPort))
		{
			sender.Credentials = new NetworkCredential(MailingSettings.SourceMail, MailingSettings.SMTPPassword);
			sender.EnableSsl = true;

			using (MailMessage payload = new MailMessage())
			{
				payload.From = new MailAddress(MailingSettings.SourceMail);
				payload.To.Add(message.Destination);
				payload.Subject = message.Subject;
				payload.Body = message.Message;
				TaskCompletionSource<object> completion = new TaskCompletionSource<object>();
				try
				{
					await sender.SendMailAsync(payload);
					return Result.Success();
				}
				catch
				{
					return Result.Failure("Ошибка при отправке сообщения по почте");
				}
			}
		}
	}
}
