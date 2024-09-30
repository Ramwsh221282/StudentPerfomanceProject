using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.StudentGroups.Module.Queries.Count;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

namespace SPerfomance.Api.Module.Facades.StudentGroups;

internal sealed class StudentGroupCountFacade : IFacade<int>
{
	public async Task<ActionResult<int>> Process()
	{
		IService<int> service = new StudentGroupsCountService(RepositoryProvider.CreateStudentGroupsRepository());
		OperationResult<int> result = await service.DoOperation();
		int count = result.Result;
		return new OkObjectResult(count);
	}
}
