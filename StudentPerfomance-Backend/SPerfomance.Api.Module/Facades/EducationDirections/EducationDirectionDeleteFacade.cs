using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Commands.Delete;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal sealed class EducationDirectionDeleteFacade : IFacade<EducationDirectionResponse>
{
	private IRepository<EducationDirection> _repository;
	private IRepositoryExpression<EducationDirection> _getDirection;
	public EducationDirectionDeleteFacade(EducationDirectionSchema schema)
	{
		_repository = RepositoryProvider.CreateDirectionsRepository();
		EducationDirectionsRepositoryObject parameter = EducationDirectionSchemaConverter.ToRepositoryObject(schema);
		_getDirection = EducationDirectionExpressionsFactory.FindDirection(parameter);
	}

	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		IService<EducationDirection> service = new EducationDirectionDeletionService
		(
			_getDirection,
			_repository
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
