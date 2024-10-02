using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Queries.Search;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherSearchFacade(TeacherSchema teacher) : IFacade<IReadOnlyCollection<TeacherResponse>>
{
	private readonly TeacherSchema _teacher = teacher;
	public async Task<ActionResult<IReadOnlyCollection<TeacherResponse>>> Process()
	{
		TeacherRepositoryObject parameter = _teacher.ToRepositoryObject();
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		IRepositoryExpression<Teacher> expression = TeachersRepositoryExpressionFactory.CreateFilter(parameter);
		IService<IReadOnlyCollection<Teacher>> service = new SearchTeachersByFilterService(expression, repository);
		return TeacherResponse.FromResult(await service.DoOperation());
	}
}
