using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;

public sealed class TeacherDepartmentDeletionService
(
	IRepositoryExpression<TeachersDepartment> expression,
	IRepository<TeachersDepartment> repository
) : IService<TeachersDepartment>
{
	private readonly TeacherDepartmentDeleteCommand _command = new TeacherDepartmentDeleteCommand(expression);
	private readonly TeacherDepartmentDeleteCommandHandler _handler = new TeacherDepartmentDeleteCommandHandler(repository);
	public async Task<OperationResult<TeachersDepartment>> DoOperation() => await _handler.Handle(_command);
}
