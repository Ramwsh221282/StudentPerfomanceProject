using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Application.Students.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Students.Module.Queries.GetGroupStudents;

internal sealed class GetGroupStudentsQuery : IQuery
{
	private readonly IRepositoryExpression<Student> _expression;
	private readonly StudentQueryRepository _repository;

	public readonly IQueryHandler<GetGroupStudentsQuery, IReadOnlyCollection<Student>> Handler;

	public GetGroupStudentsQuery(StudentsGroupSchema group, string token)
	{
		_expression = ExpressionsFactory.GetByGroup(group.ToRepositoryObject());
		_repository = new StudentQueryRepository();
		Handler = new QueryVerificaitonHandler<GetGroupStudentsQuery, IReadOnlyCollection<Student>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetGroupStudentsQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetGroupStudentsQuery, IReadOnlyCollection<Student>> handler,
			StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetGroupStudentsQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Student> students = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
