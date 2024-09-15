using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Departments.DeleteDepartment;

internal sealed class DeleteDepartmentCommandHandler
(
	IRepository<TeachersDepartment> repository,
	IRepositoryExpression<TeachersDepartment> expression
)
: CommandWithErrorBuilder, ICommandHandler<DeleteDepartmentCommand, OperationResult<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;

	public async Task<OperationResult<TeachersDepartment>> Handle(DeleteDepartmentCommand command)
	{
		TeachersDepartment? department = await _repository.GetByParameter(_expression);
		department.ValidateNullability("Кафедра не найдена", this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(department);
			return new OperationResult<TeachersDepartment>(department);
		});
	}
}
