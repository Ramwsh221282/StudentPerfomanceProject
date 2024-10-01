using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;

internal sealed class TeacherDepartmentDeleteCommand(IRepositoryExpression<TeachersDepartment> expression) : ICommand
{
	public IRepositoryExpression<TeachersDepartment> Expression { get; init; } = expression;
}
