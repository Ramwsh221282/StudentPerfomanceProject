using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationPlans.Count;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationPlansFacade;

internal sealed class EducationPlanCountFacade : IFacade<int>
{
	public async Task<ActionResult<int>> Process()
	{
		IRepository<EducationPlan> repository = new EducationPlansRepository();
		IService<int> service = new EducationPlansGetCountService(repository);
		OperationResult<int> result = await service.DoOperation();
		return new OkObjectResult(result.Result);
	}
}
