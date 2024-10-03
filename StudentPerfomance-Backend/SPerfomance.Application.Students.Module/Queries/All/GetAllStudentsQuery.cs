using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.All;

public sealed class GetAllStudentsQuery(IRepository<Student> repository) : IQuery
{
	public readonly IQueryHandler<GetAllStudentsQuery, IReadOnlyCollection<Student>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Student> repository) : IQueryHandler<GetAllStudentsQuery, IReadOnlyCollection<Student>>
	{
		private readonly IRepository<Student> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetAllStudentsQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
