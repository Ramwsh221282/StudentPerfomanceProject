using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionCountFacade : IFacade<int>
{
	private readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();
	public async Task<ActionResult<int>> Process()
	{
		int count = await _repository.Count();
		return new OkObjectResult(count);
	}
}
