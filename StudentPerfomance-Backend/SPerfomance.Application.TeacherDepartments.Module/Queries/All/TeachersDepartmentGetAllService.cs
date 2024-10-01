using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.All;

public sealed class TeachersDepartmentGetAllService(IRepository<TeachersDepartment> repository) : IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation()
	{
		IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetAll();
		return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
	}
}
