using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly TeacherQueryRepository _repository;

	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<Teacher>> Handler;

	public GetAllQuery(string token)
	{
		_repository = new TeacherQueryRepository();
		Handler = new QueryVerificaitonHandler<GetAllQuery, IReadOnlyCollection<Teacher>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetAllQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetAllQuery, IReadOnlyCollection<Teacher>> handler,
			TeacherQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetAllQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Teacher> teachers = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}
