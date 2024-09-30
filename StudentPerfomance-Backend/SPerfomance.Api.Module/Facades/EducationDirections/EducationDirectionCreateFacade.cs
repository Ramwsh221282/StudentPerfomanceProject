using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Commands.Create;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Facades.EducationDirections;

internal sealed class EducationDirectionCreateFacade : IFacade<EducationDirectionResponse>
{
	private readonly EducationDirectionSchema _schema;
	private readonly IRepository<EducationDirection> _repository;
	private readonly IRepositoryExpression<EducationDirection> _checkForCodeDublicate;

	public EducationDirectionCreateFacade(EducationDirectionSchema schema)
	{
		_schema = schema;
		_repository = RepositoryProvider.CreateDirectionsRepository();
		EducationDirectionsRepositoryObject parameter = EducationDirectionSchemaConverter.ToRepositoryObject(schema);
		_checkForCodeDublicate = EducationDirectionExpressionsFactory.FindDirectionByCode(parameter);
	}

	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		IService<EducationDirection> service = new EducationDirectionCreationService
		(
			_schema,
			_checkForCodeDublicate,
			_repository
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
