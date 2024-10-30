using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.RemoveStudentGroup;

public class RemoveStudentGroupCommandHandler
(
	IStudentGroupsRepository repository
) : ICommandHandler<RemoveStudentGroupCommand, StudentGroup>
{
	private readonly IStudentGroupsRepository _repository = repository;

	public async Task<Result<StudentGroup>> Handle(RemoveStudentGroupCommand command)
	{
		if (command.Group == null)
			return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

		await _repository.Remove(command.Group);
		return Result<StudentGroup>.Success(command.Group);
	}
}
