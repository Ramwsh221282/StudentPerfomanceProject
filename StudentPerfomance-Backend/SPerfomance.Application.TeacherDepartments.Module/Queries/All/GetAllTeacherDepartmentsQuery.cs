using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.All;

public sealed class GetAllTeacherDepartmentsQuery(IRepository<TeachersDepartment> repository) : IQuery
{
	public readonly IQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<TeachersDepartment> repository) : IQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetAllTeacherDepartmentsQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
