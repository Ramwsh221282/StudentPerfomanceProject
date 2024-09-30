using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Semesters.Module.Queries.GetCount;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Api.Module.Facades.Semesters;

internal sealed class SemestersCountFacade : IFacade<int>
{
	public async Task<ActionResult<int>> Process()
	{
		IRepository<Semester> repository = RepositoryProvider.CreateSemestersRepository();
		IService<int> service = new GetSemestersCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		return new OkObjectResult(result.Result);
	}
}
