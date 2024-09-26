using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.All;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionGetAllFacade : IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		IRepository<EducationDirection> repository = new EducationDirectionRepository();
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetAllService(repository);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
