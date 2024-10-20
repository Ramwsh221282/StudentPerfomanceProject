using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.API.Requests;
using SPerfomance.Application.Shared.Users.Module.API.Responses;
using SPerfomance.Application.Shared.Users.Module.Queries.GetCount;
using SPerfomance.Application.Shared.Users.Module.Queries.GetFiltered;
using SPerfomance.Application.Shared.Users.Module.Queries.GetPaged;

namespace SPerfomance.Application.Shared.Users.Module.API;

[ApiController]
[Route("api/users/read")]
public class UserReadApi : Controller
{
	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<UserResponse>>> GetPaged([FromQuery] GetPagedUsersRequest request)
	{
		GetPagedUsersQuery query = new GetPagedUsersQuery(request.Page, request.PageSize, request.Token);
		OperationResult<IReadOnlyCollection<UserResponse>> result = await query.Handler.Handle(query);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<UserResponse>>> Filter([FromQuery] FilterUsersRequest request)
	{
		GetFilteredQuery query = new GetFilteredQuery(
			request.Name,
			request.Surname,
			request.Thirdname,
			request.Email,
			request.token,
			request.Page,
			request.PageSize
		);

		OperationResult<IReadOnlyCollection<UserResponse>> result = await query.Handler.Handle(query);
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount([FromQuery] UserQueryRequest request)
	{
		string token = request.Token;
		GetCountQuery query = new GetCountQuery(token);
		OperationResult<int> result = await query.Handler.Handle(query);
		return result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}
}
