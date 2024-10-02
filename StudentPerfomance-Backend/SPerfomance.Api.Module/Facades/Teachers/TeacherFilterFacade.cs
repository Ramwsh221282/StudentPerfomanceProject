using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Converters.Teachers;
using SPerfomance.Api.Module.Responses.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Queries.GetFiltered;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Api.Module.Facades.Teachers;

internal sealed class TeacherFilterFacade(int page, int pageSize, TeacherSchema schema) : IFacade<IReadOnlyCollection<TeacherResponse>>
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly TeacherSchema _schema = schema;
	public async Task<ActionResult<IReadOnlyCollection<TeacherResponse>>> Process()
	{
		TeacherRepositoryObject parameter = _schema.ToRepositoryObject();
		IRepository<Teacher> repository = RepositoryProvider.CreateTeachersRepository();
		IRepositoryExpression<Teacher> expression = TeachersRepositoryExpressionFactory.CreateHasTeacher(parameter);
		IService<IReadOnlyCollection<Teacher>> service = new GetPagedFilteredTeachersService(_page, _pageSize, expression, repository);
		return TeacherResponse.FromResult(await service.DoOperation());
	}
}
