using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Student.GetStudentsByFilter;

internal sealed class GetStudentsByFilterQueryHandler
(
	IRepository<Domain.Entities.Student> repository,
	IRepositoryExpression<Domain.Entities.Student> expression
)
: IQueryHandler<GetStudentsByFilterQuery, OperationResult<IReadOnlyCollection<Domain.Entities.Student>>>
{
	private readonly IRepository<Domain.Entities.Student> _repository = repository;
	private readonly IRepositoryExpression<Domain.Entities.Student> _expression = expression;

	public async Task<OperationResult<IReadOnlyCollection<Domain.Entities.Student>>> Handle(GetStudentsByFilterQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<Domain.Entities.Student> students = await _repository.GetFilteredAndPaged(_expression, query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<Domain.Entities.Student>>(students);
		});
	}
}
