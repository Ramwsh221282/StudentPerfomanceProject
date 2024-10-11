using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.GetBySemester;

internal sealed class GetBySemesterQuery : IQuery
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly IRepositoryExpression<SemesterPlan> _expression;
	public readonly IQueryHandler<GetBySemesterQuery, IReadOnlyCollection<SemesterPlan>> Handler;

	public GetBySemesterQuery(SemesterSchema semester)
	{
		_expression = ExpressionsFactory.GetBySemester(semester.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterPlansQueryRepository repository) : IQueryHandler<GetBySemesterQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(GetBySemesterQuery query)
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}
