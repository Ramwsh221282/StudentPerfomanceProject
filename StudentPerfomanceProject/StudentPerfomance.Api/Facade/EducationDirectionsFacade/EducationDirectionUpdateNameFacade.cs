using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationDirections.UpdateName;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionUpdateNameFacade : IFacade<EducationDirectionResponse>
{
	private readonly IRepository<EducationDirection> _repository;
	private readonly EducationDirectionSchema _newSchema;
	private readonly IRepositoryExpression<EducationDirection> _getCurrentDirection;
	public EducationDirectionUpdateNameFacade(UpdateEducationDirectionNameRequest request)
	{
		_repository = new EducationDirectionRepository();
		_newSchema = request.NewSchema;
		EducationDirectionRepositoryParameter getCurrentParameter = EducationDirectionSchemaConverter.ToRepositoryParameter(request.OldSchema);
		_getCurrentDirection = EducationDirectionExpressionsFactory.FindDirection(getCurrentParameter);
	}

	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		IService<EducationDirection> service = new UpdateEducationDirectionNameService
		(
			_newSchema,
			_getCurrentDirection,
			_repository
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
