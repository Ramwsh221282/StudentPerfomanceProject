using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.Teachers.Module.Commands.Create;

public sealed class TeacherCreateCommandHandler
(
	IRepository<Teacher> teachers,
	IRepository<TeachersDepartment> departments
) : ICommandHandler<TeacherCreateCommand, OperationResult<Teacher>>
{
	private readonly IRepository<Teacher> _teachers = teachers;
	private readonly IRepository<TeachersDepartment> _departments = departments;
	public async Task<OperationResult<Teacher>> Handle(TeacherCreateCommand command)
	{
		if (!command.Validator.IsValid)
			return new OperationResult<Teacher>(command.Validator.Error);
		if (await _teachers.HasEqualRecord(command.FindDublicate))
			return new OperationResult<Teacher>(new TeacherDublicateError
			(
				command.Teacher.Name,
				command.Teacher.Surname,
				command.Teacher.Thirdname,
				command.Teacher.Job,
				command.Teacher.Condition
			).ToString());
		TeachersDepartment? department = await _departments.GetByParameter(command.FindDepartment);
		if (department == null) return new OperationResult<Teacher>(new DepartmentNotFountError().ToString());
		Teacher teacher = command.Teacher.CreateDomainObject(department);
		await _teachers.Create(teacher);
		return new OperationResult<Teacher>(teacher);
	}
}
