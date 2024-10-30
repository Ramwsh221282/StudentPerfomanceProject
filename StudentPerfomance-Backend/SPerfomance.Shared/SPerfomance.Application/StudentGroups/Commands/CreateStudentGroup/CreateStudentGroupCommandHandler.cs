using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;

public class CreateStudentGroupCommandHandler
(
	IStudentGroupsRepository repository
) : ICommandHandler<CreateStudentGroupCommand, StudentGroup>
{
	private readonly IStudentGroupsRepository _repository = repository;

	public async Task<Result<StudentGroup>> Handle(CreateStudentGroupCommand command)
	{
		if (await _repository.HasWithName(command.Name))
			return Result<StudentGroup>.Failure(StudentGroupErrors.NameDublicate(command.Name));

		Result<StudentGroup> group = StudentGroup.Create(command.Name);
		if (group.IsFailure)
			return group;

		await _repository.Insert(group.Value);
		return Result<StudentGroup>.Success(group.Value);
	}
}
