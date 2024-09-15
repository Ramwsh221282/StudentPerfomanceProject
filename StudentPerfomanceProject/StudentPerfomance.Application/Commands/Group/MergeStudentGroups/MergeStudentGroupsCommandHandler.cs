using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.MergeStudentGroups;

internal sealed class MergeStudentGroupsCommandHandler
(
	IRepository<StudentGroup> repository
)
: CommandWithErrorBuilder, ICommandHandler<MergeStudentGroupsCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<StudentGroup>> Handle(MergeStudentGroupsCommand command)
	{
		command.ValidatorA.ValidateSchema(this);
		command.ValidatorB.ValidateSchema(this);
		StudentGroup? groupA = await _repository.GetByParameter(command.ExistanceA);
		groupA.ValidateNullability("Несуществующая группа А", this);
		StudentGroup? groupB = await _repository.GetByParameter(command.ExistanceB);
		groupB.ValidateNullability("Несуществующая группа Б", this);
		return await this.ProcessAsync(async () =>
		{
			groupA.MergeWithGroup(groupB);
			await _repository.Commit();
			return new OperationResult<StudentGroup>(groupA);
		});
	}
}
