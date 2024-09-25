using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.ByPage;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionGetPagedFacade(int page, int pageSize) : IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetByPageService(_page, _pageSize, _repository);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
