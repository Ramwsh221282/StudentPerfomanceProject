using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.Teachers.Module.Commands.Delete;

public sealed class TeacherDeleteCommandHandler
(
	IRepository<Teacher> repository
) : ICommandHandler<TeacherDeleteCommand, OperationResult<Teacher>>
{
	private readonly IRepository<Teacher> _repository = repository;

	public async Task<OperationResult<Teacher>> Handle(TeacherDeleteCommand command)
	{
		Teacher? teacher = await _repository.GetByParameter(command.FindTeacher);
		if (teacher == null)
			return new OperationResult<Teacher>(new TeacherNotFoundError().ToString());
		await _repository.Remove(teacher);
		return new OperationResult<Teacher>(teacher);
	}
}
