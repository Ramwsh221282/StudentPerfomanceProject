using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.Teachers.Module.Commands.Create;

public sealed class TeacherCreateCommand : ICommand
{
	private readonly TeacherSchema _teacher;
	private readonly IRepositoryExpression<Teacher> _findDublicate;
	private readonly IRepositoryExpression<TeachersDepartment> _findDepartment;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<TeacherCreateCommand, Teacher> Handler;
	public TeacherCreateCommand
	(
		TeacherSchema teacher,
		IRepositoryExpression<Teacher> findDublicate,
		IRepositoryExpression<TeachersDepartment> findDepartment,
		IRepository<Teacher> teachers,
		IRepository<TeachersDepartment> departments
	)
	{
		_teacher = teacher;
		_findDublicate = findDublicate;
		_findDepartment = findDepartment;
		_validator = new TeacherValidator()
		.WithNameValidation(teacher)
		.WithConditionValidation(teacher)
		.WithJobTitle(teacher);
		_validator.ProcessValidation();
		Handler = new CommandHandler(teachers, departments);
	}

	internal sealed class CommandHandler(IRepository<Teacher> teachers, IRepository<TeachersDepartment> departments) : ICommandHandler<TeacherCreateCommand, Teacher>
	{
		private readonly IRepository<Teacher> _teachers = teachers;
		private readonly IRepository<TeachersDepartment> _departments = departments;
		public async Task<OperationResult<Teacher>> Handle(TeacherCreateCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<Teacher>(command._validator.Error);
			if (await _teachers.HasEqualRecord(command._findDublicate))
				return new OperationResult<Teacher>(new TeacherDublicateError
				(
					command._teacher.Name,
					command._teacher.Surname,
					command._teacher.Thirdname,
					command._teacher.Job,
					command._teacher.Condition
				).ToString());
			TeachersDepartment? department = await _departments.GetByParameter(command._findDepartment);
			if (department == null) return new OperationResult<Teacher>(new DepartmentNotFountError().ToString());
			Teacher teacher = command._teacher.CreateDomainObject(department);
			await _teachers.Create(teacher);
			return new OperationResult<Teacher>(teacher);
		}
	}
}
