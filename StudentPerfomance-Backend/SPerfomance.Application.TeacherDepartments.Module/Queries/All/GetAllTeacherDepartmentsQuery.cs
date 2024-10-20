using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.All;

internal sealed class GetAllTeacherDepartmentsQuery : IQuery
{
	private readonly TeacherDepartmentsQueryRepository _repository;
	public readonly IQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler;

	public GetAllTeacherDepartmentsQuery(string token)
	{
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryVerificaitonHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> handler,
			TeacherDepartmentsQueryRepository repository)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetAllTeacherDepartmentsQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
