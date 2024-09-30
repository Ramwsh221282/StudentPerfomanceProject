using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Queries.All;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal sealed class EducationDirectionGetAllFacade : IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		IRepository<EducationDirection> repository = RepositoryProvider.CreateDirectionsRepository();
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetAllService(repository);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
