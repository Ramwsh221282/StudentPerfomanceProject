using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Searched;

internal sealed class SearchTeachersDepartmentsQuery : IQuery
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression;
	private readonly TeacherDepartmentsQueryRepository _repository;

	public readonly IQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler;

	public SearchTeachersDepartmentsQuery(DepartmentSchema department, string token)
	{
		_expression = ExpressionsFactory.GetDepartment(department.ToRepositoryObject());
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryVerificaitonHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>(
			token,
			User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<SearchTeachersDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> handler,
			TeacherDepartmentsQueryRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(SearchTeachersDepartmentsQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
