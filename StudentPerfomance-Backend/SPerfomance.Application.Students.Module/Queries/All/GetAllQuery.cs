using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Students.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly StudentQueryRepository _repository;

	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<Student>> Handler;

	public GetAllQuery(string token)
	{
		_repository = new StudentQueryRepository();
		Handler = new QueryVerificaitonHandler<GetAllQuery, IReadOnlyCollection<Student>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetAllQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetAllQuery, IReadOnlyCollection<Student>> handler,
			StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetAllQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Student> students = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
