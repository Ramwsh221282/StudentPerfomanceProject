using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Count;

public sealed class GetTeachersDepartmentsCountQuery(IRepository<TeachersDepartment> repository) : IQuery
{
	public readonly IQueryHandler<GetTeachersDepartmentsCountQuery, int> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<TeachersDepartment> repository) : IQueryHandler<GetTeachersDepartmentsCountQuery, int>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<int>> Handle(GetTeachersDepartmentsCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
