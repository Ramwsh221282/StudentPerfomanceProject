using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Commands.EducationDirections.Delete;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionDeleteFacade : IFacade<EducationDirectionResponse>
{
	private IRepository<EducationDirection> _repository;
	private IRepositoryExpression<EducationDirection> _getDirection;
	public EducationDirectionDeleteFacade(DeleteEducationDirectionRequest request)
	{
		_repository = new EducationDirectionRepository();
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(request.Direction);
		_getDirection = EducationDirectionExpressionsFactory.FindDirection(parameter);
	}

	public async Task<ActionResult<EducationDirectionResponse>> Process()
	{
		IService<EducationDirection> service = new DeleteEducationDirectionService
		(
			_getDirection,
			_repository
		);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
