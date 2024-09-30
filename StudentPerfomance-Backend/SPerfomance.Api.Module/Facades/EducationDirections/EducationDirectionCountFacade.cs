using Microsoft.AspNetCore.Mvc;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal sealed class EducationDirectionCountFacade : IFacade<int>
{
	private readonly IRepository<EducationDirection> _repository = RepositoryProvider.CreateDirectionsRepository();
	public async Task<ActionResult<int>> Process()
	{
		int count = await _repository.Count();
		return new OkObjectResult(count);
	}
}
