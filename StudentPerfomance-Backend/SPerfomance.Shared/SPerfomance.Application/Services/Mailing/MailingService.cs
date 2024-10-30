using System.Net;
using System.Net.Mail;

using SPerfomance.Application.Services.Mailing.MailingMessages;

namespace SPerfomance.Application.Services.Mailing;

public class MailingService : IMailingService
{
	public async Task SendMessage(MailingMessage message)
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
				try { await sender.SendMailAsync(payload); }
				catch { }
			}
		}
	}
}
