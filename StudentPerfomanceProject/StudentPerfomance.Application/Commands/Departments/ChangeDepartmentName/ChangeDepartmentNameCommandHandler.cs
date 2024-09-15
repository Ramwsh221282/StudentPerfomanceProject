using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Departments.ChangeDepartmentName;

internal sealed class ChangeDepartmentNameCommandHandler
(
	 IRepository<TeachersDepartment> repository,
	 IRepositoryExpression<TeachersDepartment> existance,
	 IRepositoryExpression<TeachersDepartment> dublicate
)
: CommandWithErrorBuilder, ICommandHandler<ChangeDepartmentNameCommand, OperationResult<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	private readonly IRepositoryExpression<TeachersDepartment> _existance = existance;
	private readonly IRepositoryExpression<TeachersDepartment> _dublicate = dublicate;
	public async Task<OperationResult<TeachersDepartment>> Handle(ChangeDepartmentNameCommand command)
	{
		command.Validator.ValidateSchema(this);
		TeachersDepartment? department = await _repository.GetByParameter(_existance);
		department.ValidateNullability("Несуществующая кафедра", this);
		if (department != null && !department.IsSameAs(command.NewData.Name))
			await _repository.ValidateExistance(_dublicate, "Кафедра с таким названием уже существует", this);
		return await this.ProcessAsync(async () =>
		{
			department.ChangeDepartmentName(command.NewData.Name);
			await _repository.Commit();
			return new OperationResult<TeachersDepartment>(department);
		});
	}
}
