using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.CollectionPagedFilters;
using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionFilterByNameFacade : EducationDirectionFilterFacade, IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	public EducationDirectionFilterByNameFacade(FilterEducationDirectionRequest request, FilterConstraint constraint, int page, int pageSize)
	: base(request, constraint, page, pageSize)
	{ }

	public new async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_filterSchema);
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterByNameExpression(parameter);
		IService<IReadOnlyCollection<EducationDirection>> service = EducationDirectionGetPagedCollectionByFilterBuilder.Build(_constraint, _page, _pageSize, _repository, expression);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
