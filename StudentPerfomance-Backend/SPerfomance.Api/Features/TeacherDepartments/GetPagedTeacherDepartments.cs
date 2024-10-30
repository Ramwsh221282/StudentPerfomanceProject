using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class GetPagedTeacherDepartments
{
	public record Request(PaginationContract Pagination, TokenContract Token);

	public sealed class Endpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app) =>
			app.MapPost($"{TeacherDepartmentsTags.Api}/byPage", Handler).WithTags(TeacherDepartmentsTags.Tag);
	}

	public static async Task<IResult> Handler(Request request, IUsersRepository users, ITeacherDepartmentsRepository repository)
	{
		if (!await new UserVerificationService(users).IsVerified(request.Token, UserRole.Administrator))
			return Results.BadRequest(UserTags.UnauthorizedError);

		IReadOnlyCollection<TeachersDepartments> departments = await repository.GetPaged(request.Pagination.Page, request.Pagination.PageSize);
		return Results.Ok(departments.Select(d => d.MapFromDomain()));
	}
}
