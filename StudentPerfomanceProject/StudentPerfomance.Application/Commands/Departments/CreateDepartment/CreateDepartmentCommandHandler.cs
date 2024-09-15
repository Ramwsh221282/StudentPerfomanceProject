using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Departments.CreateDepartment;

internal sealed class CreateDepartmentCommandHandler
(
	IRepository<TeachersDepartment> repository,
	IRepositoryExpression<TeachersDepartment> expression
)
: CommandWithErrorBuilder, ICommandHandler<CreateDepartmentCommand, OperationResult<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;
	public async Task<OperationResult<TeachersDepartment>> Handle(CreateDepartmentCommand command)
	{
		command.Validator.ValidateSchema(this);
		await _repository.ValidateExistance(_expression, "Такая кафедра уже существует", this);
		return await this.ProcessAsync(async () =>
		{
			TeachersDepartment department = TeachersDepartment.Create(Guid.NewGuid(), command.Department.Name).Value;
			await _repository.Create(department);
			return new OperationResult<TeachersDepartment>(department);
		});
	}
}
