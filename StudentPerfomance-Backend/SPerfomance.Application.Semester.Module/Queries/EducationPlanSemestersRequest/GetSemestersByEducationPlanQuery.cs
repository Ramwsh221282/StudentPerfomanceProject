using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Semester.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Semester.Module.Queries.EducationPlanSemestersRequest;

internal sealed class GetSemestersByEducationPlanQuery : IQuery
{
	private readonly IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> _expression;
	private readonly SemesterQueryRepository _repository;
	public readonly IQueryHandler<GetSemestersByEducationPlanQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> Handler;

	public GetSemestersByEducationPlanQuery(EducationPlanDTO plan, string token)
	{
		_expression = ExpressionsFactory.EducationPlanSemesters(plan.ToSchema().ToRepositoryObject());
		_repository = new SemesterQueryRepository();
		Handler = new QueryVerificaitonHandler<GetSemestersByEducationPlanQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler
	: DecoratedQueryHandler<GetSemestersByEducationPlanQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>
	{
		private readonly SemesterQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetSemestersByEducationPlanQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> handler,
			SemesterQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>> Handle(GetSemestersByEducationPlanQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester> semesters = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(semesters);
		}
	}
}
