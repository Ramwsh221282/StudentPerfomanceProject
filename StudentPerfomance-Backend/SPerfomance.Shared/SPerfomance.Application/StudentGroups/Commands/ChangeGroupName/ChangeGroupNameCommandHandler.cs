using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.ChangeGroupName;

public class ChangeGroupNameCommandHandler
(
	IStudentGroupsRepository repository
) : ICommandHandler<ChangeGroupNameCommand, StudentGroup>
{
	private readonly IStudentGroupsRepository _repository = repository;

	public async Task<Result<StudentGroup>> Handle(ChangeGroupNameCommand command)
	{
		if (command.Group == null)
			return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

		Result<StudentGroup> update = command.Group.ChangeName(command.NewName);
		if (update.IsFailure)
			return update;

		await _repository.Update(update.Value);
		return update;
	}
}
