using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Converters;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Application.Queries.EducationDirections.CollectionFilters;
using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Api.Facade.EducationDirectionsFacade;

internal sealed class EducationDirectionSearchByCodeFacade : EducationDirectionSearchFacade, IFacade<IReadOnlyCollection<EducationDirectionResponse>>
{
	public EducationDirectionSearchByCodeFacade(FilterEducationDirectionRequest request, FilterConstraint constraint) : base(request, constraint) { }
	public new async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Process()
	{
		EducationDirectionRepositoryParameter parameter = EducationDirectionSchemaConverter.ToRepositoryParameter(_filterSchema);
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterByCodeExpression(parameter);
		IService<IReadOnlyCollection<EducationDirection>> service = EducationDirectionGetCollectionByFilterBuilder.Build(_constraint, _repository, expression);
		return EducationDirectionResponse.FromResult(await service.DoOperation());
	}
}
