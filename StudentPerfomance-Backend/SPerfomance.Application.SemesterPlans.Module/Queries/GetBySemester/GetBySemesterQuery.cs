using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.GetBySemester;

internal sealed class GetBySemesterQuery : IQuery
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly IRepositoryExpression<SemesterPlan> _expression;
	public readonly IQueryHandler<GetBySemesterQuery, IReadOnlyCollection<SemesterPlan>> Handler;

	public GetBySemesterQuery(SemesterSchema semester, string token)
	{
		_expression = ExpressionsFactory.GetBySemester(semester.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryVerificaitonHandler<GetBySemesterQuery, IReadOnlyCollection<SemesterPlan>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetBySemesterQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetBySemesterQuery, IReadOnlyCollection<SemesterPlan>> handler,
			SemesterPlansQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(GetBySemesterQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}
