using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Semester.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Queries.EducationPlanSemestersRequest;

internal sealed class GetSemestersByEducationPlanQuery : IQuery
{
	private readonly IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> _expression;
	private readonly SemesterQueryRepository _repository;
	public readonly IQueryHandler<GetSemestersByEducationPlanQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> Handler;

	public GetSemestersByEducationPlanQuery(EducationPlanSchema schema)
	{
		_expression = ExpressionsFactory.EducationPlanSemesters(schema.ToRepositoryObject());
		_repository = new SemesterQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterQueryRepository repository) :
	IQueryHandler<GetSemestersByEducationPlanQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>
	{
		private readonly SemesterQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>> Handle(GetSemestersByEducationPlanQuery query)
		{
			IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester> semesters = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(semesters);
		}
	}
}
