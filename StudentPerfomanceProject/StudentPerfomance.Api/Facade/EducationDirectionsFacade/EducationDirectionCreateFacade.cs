using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationDirections.Create;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionCreateFacade : IFacade<EducationDirectionResponse>
{
	private readonly IRepository<EducationDirection> _repository;
	private readonly EducationDirectionSchema _direction;
	private readonly IRepositoryExpression<EducationDirection> _checkForDublicate;
	private readonly IRepositoryExpression<EducationDirection> _checkForCodeDublicate;

	public EducationDirectionCreateFacade(CreateEducationDirectionRequest request)
	{
		_repository = new EducationDirectionRepository();
		_direction = request.Direction;
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_direction);
		_checkForDublicate = EducationDirectionExpressionsFactory.FindDirection(parameter);
		_checkForCodeDublicate = EducationDirectionExpressionsFactory.FindDirectionByCode(parameter);
	}

	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		IService<EducationDirection> service = new CreateEducationDirectionService
		(
			_direction,
			_checkForDublicate,
			_checkForCodeDublicate,
			_repository
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
