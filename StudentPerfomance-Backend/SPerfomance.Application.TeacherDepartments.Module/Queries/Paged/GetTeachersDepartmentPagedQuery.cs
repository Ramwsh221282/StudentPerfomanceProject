using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;

public sealed class GetTeachersDepartmentPagedQuery(int page, int pageSize, IRepository<TeachersDepartment> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public readonly IQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<TeachersDepartment> repository) : IQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetTeachersDepartmentPagedQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
