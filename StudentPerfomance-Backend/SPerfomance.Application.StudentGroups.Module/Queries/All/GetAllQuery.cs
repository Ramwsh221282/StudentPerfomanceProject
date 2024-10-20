using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly StudentGroupQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<StudentGroup>> Handler;

	public GetAllQuery(string token)
	{
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryVerificaitonHandler<GetAllQuery, IReadOnlyCollection<StudentGroup>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetAllQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetAllQuery, IReadOnlyCollection<StudentGroup>> handler,
			StudentGroupQueryRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetAllQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<StudentGroup> groups = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
