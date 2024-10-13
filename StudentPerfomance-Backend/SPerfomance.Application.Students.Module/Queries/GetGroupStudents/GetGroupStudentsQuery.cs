using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.GetGroupStudents;

internal sealed class GetGroupStudentsQuery : IQuery
{
	private readonly IRepositoryExpression<Student> _expression;
	private readonly StudentQueryRepository _repository;

	public readonly IQueryHandler<GetGroupStudentsQuery, IReadOnlyCollection<Student>> Handler;

	public GetGroupStudentsQuery(StudentsGroupSchema group)
	{
		_expression = ExpressionsFactory.GetByGroup(group.ToRepositoryObject());
		_repository = new StudentQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentQueryRepository repository) : IQueryHandler<GetGroupStudentsQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetGroupStudentsQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
