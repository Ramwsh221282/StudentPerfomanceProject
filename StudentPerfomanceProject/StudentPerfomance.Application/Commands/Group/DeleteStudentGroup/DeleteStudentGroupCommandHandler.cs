using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.DeleteStudentGroup;

internal sealed class DeleteStudentGroupCommandHandler
(
	IRepository<StudentGroup> repository
)
: CommandWithErrorBuilder, ICommandHandler<DeleteStudentGroupCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<StudentGroup>> Handle(DeleteStudentGroupCommand command)
	{
		StudentGroup? group = await _repository.GetByParameter(command.Expression);
		group.ValidateNullability("Группа не найдена", this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(group);
			return new OperationResult<StudentGroup>(group);
		});
	}
}
