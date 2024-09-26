using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Responses.EducationPlans;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationPlans.ByPage;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationPlansFacade;

internal sealed class EducationPlanGetPagedFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<EducationPlanResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> Process()
	{
		IRepository<EducationPlan> repository = new EducationPlansRepository();
		IService<IReadOnlyCollection<EducationPlan>> service = new EducationPlansGetPagedService(_page, _pageSize, repository);
		return EducationPlanResponse.FromResult(await service.DoOperation());
	}
}
