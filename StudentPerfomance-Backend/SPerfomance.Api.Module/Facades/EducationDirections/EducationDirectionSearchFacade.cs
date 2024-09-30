using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Queries.GetFiltered;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal class EducationDirectionSearchFacade(EducationDirectionSchema request) : IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	private readonly EducationDirectionSchema _filterSchema = request;
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		EducationDirectionsRepositoryObject parameter = EducationDirectionSchemaConverter.ToRepositoryObject(_filterSchema);
		IService<IReadOnlyCollection<EducationDirection>> service = new EducationDirectionGetByFilter
		(
			RepositoryProvider.CreateDirectionsRepository(),
			EducationDirectionExpressionsFactory.FilterExpression(parameter)
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
