using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.Queries.EducationDirections.CollectionPagedFilters;
using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal class EducationDirectionFilterFacade
(
	FilterEducationDirectionRequest request,
	FilterConstraint constraint,
	int page,
	int pageSize
)
: IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	protected readonly EducationDirectionSchema _filterSchema = request.Direction;
	protected readonly FilterConstraint _constraint = constraint;
	protected readonly int _page = page;
	protected readonly int _pageSize = pageSize;
	protected readonly IRepository<EducationDirection> _repository = new EducationDirectionRepository();

	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_filterSchema);
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterExpression(parameter);
		IService<IReadOnlyCollection<EducationDirection>> service = EducationDirectionGetPagedCollectionByFilterBuilder.Build(_constraint, _page, _pageSize, _repository, expression);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
