using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionUpdateFacade : IFacade<EducationDirectionResponse>
{
	private readonly EducationDirectionSchema _oldSchema;
	private readonly EducationDirectionSchema _newSchema;
	private readonly IRepository<EducationDirection> _repository;
	private readonly IRepositoryExpression<EducationDirection> _findDirection;
	private readonly IRepositoryExpression<EducationDirection> _checkCodeDublicate;
	public EducationDirectionUpdateFacade(UpdateEducationDirectionRequest request)
	{
		_oldSchema = request.OldSchema;
		_newSchema = request.NewSchema;
		_repository = new EducationDirectionRepository();
		_findDirection = EducationDirectionExpressionsFactory.FindDirection(EducationDirectionSchemaConverter.ToRepositoryParameter(_oldSchema));
		_checkCodeDublicate = EducationDirectionExpressionsFactory.FindDirectionByCode(EducationDirectionSchemaConverter.ToRepositoryParameter(_newSchema));
	}

	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		IService<EducationDirection> service = new UpdateEducationDirectionService
		(
			_oldSchema,
			_newSchema,
			_findDirection,
			_checkCodeDublicate,
			_repository
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
