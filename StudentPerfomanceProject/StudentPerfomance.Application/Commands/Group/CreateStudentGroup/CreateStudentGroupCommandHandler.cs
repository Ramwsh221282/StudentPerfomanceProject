using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Application.Commands.Group.CreateStudentGroup;

internal sealed class CreateStudentGroupCommandHandler
(
	IRepository<StudentGroup> repository
)
: CommandWithErrorBuilder, ICommandHandler<CreateStudentGroupCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<StudentGroup> _repository = repository;
	public async Task<OperationResult<StudentGroup>> Handle(CreateStudentGroupCommand command)
	{
		command.Validator.ValidateSchema(this);
		await _repository.ValidateExistance(command.Expression, "Такая группа уже существует", this);
		return await this.ProcessAsync(async () =>
		{
			GroupName name = GroupName.Create(command.Group.Name).Value;
			StudentGroup group = StudentGroup.Create(Guid.NewGuid(), name).Value;
			await _repository.Create(group);
			return new OperationResult<StudentGroup>(group);
		});
	}
}
