using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationPlans.Module.Queries.Count;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Api.Module.Facades.EducationPlans;

internal sealed class EducationPlanCountFacade : IFacade<int>
{
	public async Task<ActionResult<int>> Process()
	{
		IRepository<EducationPlan> repository = RepositoryProvider.CreateEducationPlansRepository();
		IService<int> service = new EducationPlansGetCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		return new OkObjectResult(result.Result);
	}
}
