using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Commands.Update;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal sealed class EducationDirectionUpdateFacade
(
	EducationDirectionSchema oldDirection,
	EducationDirectionSchema newDirection
)
: IFacade<EducationDirectionResponse>
{
	private readonly EducationDirectionSchema _oldSchema = oldDirection;
	private readonly EducationDirectionSchema _newSchema = newDirection;
	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		EducationDirectionsRepositoryObject oldDirection = EducationDirectionSchemaConverter.ToRepositoryObject(_oldSchema);
		EducationDirectionsRepositoryObject newDirection = EducationDirectionSchemaConverter.ToRepositoryObject(_newSchema);
		IService<EducationDirection> service = new EducationDirectionUpdateService
		(
			_newSchema,
			EducationDirectionExpressionsFactory.FindDirection(oldDirection),
			EducationDirectionExpressionsFactory.FindDirectionByCode(newDirection),
			RepositoryProvider.CreateDirectionsRepository()
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
