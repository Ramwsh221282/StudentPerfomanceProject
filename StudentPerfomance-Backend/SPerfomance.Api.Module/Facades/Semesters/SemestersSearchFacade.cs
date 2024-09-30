using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.Semesters;
using SPerfomance.Api.Module.Responses.Semesters;
using SPerfomance.Application.Semesters.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Api.Module.Facades.Semesters;

internal sealed class SemestersSearchFacade(SemesterSchema semester) : IFacade<IReadOnlyCollection<SemesterResponse>>
{
	private readonly SemesterSchema _semester = semester;
	public async Task<ActionResult<IReadOnlyCollection<SemesterResponse>>> Process()
	{
		IRepository<Semester> repository = RepositoryProvider.CreateSemestersRepository();
		SemestersRepositoryObject parameter = SemesterSchemaConverter.ToRepositoryParameter(_semester);
		IRepositoryExpression<Semester> expression = SemesterExpressionsFactory.CreateFilter(parameter);
		IService<IReadOnlyCollection<Semester>> service = new SearchSemestersService(repository, expression);
		return SemesterResponse.FromResult(await service.DoOperation());
	}
}
