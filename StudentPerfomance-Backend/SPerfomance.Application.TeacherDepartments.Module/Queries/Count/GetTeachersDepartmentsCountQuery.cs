using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Count;

internal sealed class GetTeachersDepartmentsCountQuery : IQuery
{
	private readonly TeacherDepartmentsQueryRepository _repository;
	public readonly IQueryHandler<GetTeachersDepartmentsCountQuery, int> Handler;

	public GetTeachersDepartmentsCountQuery(string token)
	{
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryVerificaitonHandler<GetTeachersDepartmentsCountQuery, int>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetTeachersDepartmentsCountQuery, int>
	{
		private readonly TeacherDepartmentsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetTeachersDepartmentsCountQuery, int> handler,
			TeacherDepartmentsQueryRepository repository)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<int>> Handle(GetTeachersDepartmentsCountQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
