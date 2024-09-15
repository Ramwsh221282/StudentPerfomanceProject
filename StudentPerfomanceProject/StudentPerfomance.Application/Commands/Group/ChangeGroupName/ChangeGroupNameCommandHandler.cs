using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Application.Commands.Group.ChangeGroupName;

internal sealed class ChangeGroupNameCommandHandler
(
	IRepository<StudentGroup> repository
)
: CommandWithErrorBuilder, ICommandHandler<ChangeGroupNameCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<StudentGroup>> Handle(ChangeGroupNameCommand command)
	{
		command.Validator.ValidateSchema(this);
		await _repository.ValidateExistance(command.Dublicate, "Группа с таким названием уже существует", this);
		StudentGroup? group = await _repository.GetByParameter(command.Existance);
		group.ValidateNullability("Несуществующая группа", this);
		return await this.ProcessAsync(async () =>
		{
			GroupName name = GroupName.Create(command.Group.Name).Value;
			group.ChangeGroupName(name);
			await _repository.Commit();
			return new OperationResult<StudentGroup>(group);
		});
	}
}
