using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Delete;

public sealed class TeacherDeleteCommand(IRepositoryExpression<Teacher> findTeacher) : ICommand
{
	public IRepositoryExpression<Teacher> FindTeacher { get; init; } = findTeacher;
}
