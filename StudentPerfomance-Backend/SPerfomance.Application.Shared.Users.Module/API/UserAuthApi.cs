using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Authentication;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.SharedServices.Auth;
using SPerfomance.Application.Shared.Module.SharedServices.Auth.Errors;
using SPerfomance.Application.Shared.Users.Module.API.Requests;
using SPerfomance.Application.Shared.Users.Module.Commands.Login;
using SPerfomance.Application.Shared.Users.Module.DTOs;

namespace SPerfomance.Application.Shared.Users.Module.API;

[ApiController]
[Route("api/auth")]
public class UserAuthApi : Controller
{
	[HttpPost("login")]
	public async Task<ActionResult<AuthenticatedUser>> LoginUser([FromBody] UserLoginRequest request)
	{
		UserLoginDTO user = request.User;
		LoginCommand command = new LoginCommand(user);
		OperationResult<AuthenticatedUser> result = await command.Handler.Handle(command);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result) :
			new OkObjectResult(result.Result);
	}

	[HttpPost("verify")]
	public async Task<ActionResult<AuthenticatedUser>> VerifyUser(string? Token)
	{
		if (string.IsNullOrWhiteSpace(Token))
			return await Task.FromResult(new BadRequestObjectResult(new UserSessionExpiredError().ToString()));

		JwtVerificationObject verification = new JwtVerificationObject(Token);
		if (verification.IsExpired)
			return await Task.FromResult(new BadRequestObjectResult(new UserSessionExpiredError().ToString()));

		return Ok();
	}
}
