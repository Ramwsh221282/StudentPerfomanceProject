using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.API.Requests;
using SPerfomance.Application.Shared.Users.Module.API.Responses;
using SPerfomance.Application.Shared.Users.Module.Commands.RegisterUser;
using SPerfomance.Application.Shared.Users.Module.Commands.Remove;
using SPerfomance.Application.Shared.Users.Module.Commands.UpdateEmail;
using SPerfomance.Application.Shared.Users.Module.Commands.UpdatePassword;
using SPerfomance.Application.Shared.Users.Module.DTOs;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.API;

[ApiController]
[Route("api/users/management")]
public class UserManagementApi : Controller
{
	[HttpPost("password-update")]
	public async Task<ActionResult<string>> UpdatePassword([FromBody] UpdatePasswordRequest request)
	{
		UserLoginDTO user = request.User;
		string updatedPassword = request.newPassword;
		UpdatePasswordCommand command = new UpdatePasswordCommand(user, updatedPassword);
		OperationResult<User> result = await command.Handler.Handle(command);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult("Ваш пароль изменён");
	}

	[HttpPost("email-update")]
	public async Task<ActionResult<string>> UpdateEmail([FromBody] UpdateEmailRequest request)
	{
		UserLoginDTO user = request.User;
		string updatedEmail = request.newEmail;
		UpdateEmailCommand command = new UpdateEmailCommand(user, updatedEmail);
		OperationResult<User> result = await command.Handler.Handle(command);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult($"Новая почта для входа: {result.Result.Email.Email}");
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<UserResponse>> Remove([FromBody] UserActionRequest request)
	{
		UserActionDTO user = request.User;
		string token = request.Token;
		RemoveUserCommand command = new RemoveUserCommand(user, token);
		OperationResult<UserResponse> result = await command.Handler.Handle(command);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}

	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<UserResponse>> Create([FromBody] UserActionRequest request)
	{
		UserActionDTO user = request.User;
		string token = request.Token;
		RegisterUserCommand command = new RegisterUserCommand(user, token);
		OperationResult<UserResponse> result = await command.Handler.Handle(command);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}
}
