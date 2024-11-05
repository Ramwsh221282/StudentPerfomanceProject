using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;

namespace SPerfomance.Api.Features.Users;

public static class UsersAuth
{
	public record Request(string Email, string Password);

	public record Response(string Name, string Surname, string? Patronymic, string Email, string Role, string Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app)
		{
			app.MapPost($"{UserTags.App}/login", Handler).WithTags(UserTags.Tag);
		}
	}

	public static async Task<IResult> Handler(
		Request request,
		IUsersRepository repository,
		IPasswordHasher hasher,
		IJwtTokenService service
	)
	{
		User? user = await repository.GetByEmail(request.Email);
		if (user == null)
			return Results.NotFound(UserErrors.NotFound());

		bool isVerified = hasher.Verify(request.Password, user.HashedPassword);
		if (!isVerified)
			return Results.BadRequest(UserErrors.PasswordInvalid());

		string token = service.GenerateToken(user);

		return Results.Ok(new Response(
			user.Name.Name,
			user.Name.Surname,
			user.Name.Patronymic,
			user.Email.Email,
			user.Role.Role,
			token
		));
	}
}
