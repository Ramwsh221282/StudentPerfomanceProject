using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;

internal sealed class SearchTeachersDepartmentsQuery : IQuery
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression;
	private readonly TeacherDepartmentsQueryRepository _repository;

	public readonly IQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler;

	public SearchTeachersDepartmentsQuery(DepartmentSchema department)
	{
		_expression = ExpressionsFactory.GetDepartment(department.ToRepositoryObject());
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherDepartmentsQueryRepository repository) : IQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(SearchTeachersDepartmentsQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
