using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Student.DeleteStudent;

internal sealed class DeleteStudentCommandHandler
(
	IRepository<Domain.Entities.Student> repository,
	IRepositoryExpression<Domain.Entities.Student> expression
)
: CommandWithErrorBuilder, ICommandHandler<DeleteStudentCommand, OperationResult<Domain.Entities.Student>>
{
	private readonly IRepository<Domain.Entities.Student> _repository = repository;
	private readonly IRepositoryExpression<Domain.Entities.Student> _expression = expression;
	public async Task<OperationResult<Domain.Entities.Student>> Handle(DeleteStudentCommand command)
	{
		command.Validation.ValidateSchema(this);
		Domain.Entities.Student? student = await _repository.GetByParameter(_expression);
		student.ValidateNullability("Несуществующий студент", this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(student);
			return new OperationResult<Domain.Entities.Student>(student);
		});
	}
}
