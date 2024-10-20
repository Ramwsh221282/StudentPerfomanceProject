using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;

internal sealed class GetTeachersDepartmentPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly TeacherDepartmentsQueryRepository _repository;
	public readonly IQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>> Handler;
	public GetTeachersDepartmentPagedQuery(int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryVerificaitonHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>>(
			token,
			User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}
	internal sealed class QueryHandler : DecoratedQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>> handler,
			TeacherDepartmentsQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetTeachersDepartmentPagedQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
