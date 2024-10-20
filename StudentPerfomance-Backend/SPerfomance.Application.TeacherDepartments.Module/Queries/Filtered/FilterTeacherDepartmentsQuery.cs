using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Filtered;

internal sealed class FilterTeacherDepartmentsQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly IRepositoryExpression<TeachersDepartment> _expression;
	private readonly TeacherDepartmentsQueryRepository _repository;

	public readonly IQueryHandler<FilterTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler;

	public FilterTeacherDepartmentsQuery(
		DepartmentSchema department,
		int page,
		int pageSize,
		string token)
	{
		_expression = ExpressionsFactory.Filter(department.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryVerificaitonHandler<FilterTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>(
			token,
			User.Admin);
		Handler = new QueryHandler(
			Handler,
			_repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<FilterTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<FilterTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> handler,
			TeacherDepartmentsQueryRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(FilterTeacherDepartmentsQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFilteredAndPaged(
				query._expression,
				query._page,
				query._pageSize);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
