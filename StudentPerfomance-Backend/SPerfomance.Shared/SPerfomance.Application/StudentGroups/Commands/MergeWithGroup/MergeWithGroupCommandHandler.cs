using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;

public class MergeWithGroupCommandHandler
(
	IStudentGroupsRepository repository
)
 : ICommandHandler<MergeWithGroupCommand, StudentGroup>
{
	private readonly IStudentGroupsRepository _repository = repository;

	public async Task<Result<StudentGroup>> Handle(MergeWithGroupCommand command)
	{
		if (command.Initial == null || command.Target == null)
			return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

		Result<StudentGroup> mergedGroup = command.Initial.MergeWithGroup(command.Target);
		await _repository.UpdateMerge(mergedGroup.Value, command.Target);
		return Result<StudentGroup>.Success(mergedGroup.Value);
	}
}
