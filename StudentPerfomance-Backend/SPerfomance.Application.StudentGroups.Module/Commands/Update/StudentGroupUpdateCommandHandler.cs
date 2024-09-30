using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.ValueObjects;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Update;

internal sealed class StudentGroupUpdateCommandHandler
(
	IRepository<StudentGroup> repository
) : ICommandHandler<StudentGroupUpdateCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<StudentGroup>> Handle(StudentGroupUpdateCommand command)
	{
		if (await _repository.HasEqualRecord(command.FindNameDublicate))
			return new OperationResult<StudentGroup>(new GroupDublicateNameError(command.NewSchema.NameInfo).ToString());
		StudentGroup? group = await _repository.GetByParameter(command.FindInitialGroup);
		if (group == null)
			return new OperationResult<StudentGroup>(new GroupNotFoundError().ToString());
		if (!command.Validator.IsValid)
			return new OperationResult<StudentGroup>(command.Validator.Error);
		GroupName updatedName = command.NewSchema.CreateGroupName();
		group.ChangeGroupName(updatedName);
		return new OperationResult<StudentGroup>(group);
	}
}
