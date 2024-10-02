using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Teachers.Module.Queries.Count;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherCountFacade : IFacade<int>
{
	public async Task<ActionResult<int>> Process()
	{
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		IService<int> service = new GetTeachersCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		int count = result.Result;
		return new OkObjectResult(count);
	}
}
