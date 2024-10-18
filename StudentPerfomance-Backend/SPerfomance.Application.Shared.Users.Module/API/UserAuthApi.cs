using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.Shared.Module.Authentication;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Commands.Create;
using SPerfomance.Application.Shared.Users.Module.DTOs;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Application.Shared.Users.Module.API.Requests;
using SPerfomance.Application.Shared.Users.Module.Commands.Login;
using SPerfomance.Application.Shared.Module.SharedServices.Mailing;
using SPerfomance.Application.Shared.Module.SharedServices.Mailing.MailingMessages;
using System.Text;
using CSharpFunctionalExtensions;

namespace SPerfomance.Application.Shared.Users.Module.API;

[ApiController]
[Route("/sperfomance/api/auth")]
public class UserAuthApi : Controller
{
	private readonly MailingService _mailingService = new MailingService();

	[HttpPost("register")]
	public async Task<ActionResult<string>> RegisterUser([FromBody] UserRegistrationRequest request)
	{
		UserRegistrationDTO user = request.User;
		CreateUserCommand command = new CreateUserCommand(user);
		OperationResult<(User, string)> result = await command.Handler.Handle(command);
		if (result.Result.Item1 == null || result.IsFailed)
			return new BadRequestObjectResult(result.Error);
		User registeredUser = result.Result.Item1;
		string password = result.Result.Item2;
		StringBuilder bodyBuilder = new StringBuilder();
		bodyBuilder.AppendLine($"EMAIL авторизации: {registeredUser.Email.Email}");
		bodyBuilder.AppendLine($"Пароль авторизации: {password}");
		MailingMessage message = new UserRegistrationMessage(registeredUser.Email.Email, bodyBuilder.ToString());
		Result completion = await _mailingService.SendMessage(message);
		return completion.IsFailure ?
			new BadRequestObjectResult(completion.Error) :
			new OkObjectResult($"Регистрация успешна. Данные авторизации отправлены на указанную почту при регистрации");
	}

	[HttpPost("login")]
	public async Task<ActionResult<AuthenticatedUser>> LoginUser([FromBody] UserLoginRequest request)
	{
		UserLoginDTO user = request.User;
		LoginCommand command = new LoginCommand(user);
		OperationResult<AuthenticatedUser> result = await command.Handler.Handle(command);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}
}
