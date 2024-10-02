using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Commands.Create;

public sealed class TeacherCreateCommand : ICommand
{
	public TeacherSchema Teacher { get; init; }
	public IRepositoryExpression<Teacher> FindDublicate { get; init; }
	public IRepositoryExpression<TeachersDepartment> FindDepartment { get; init; }
	public ISchemaValidator Validator { get; init; }
	public TeacherCreateCommand
	(
		TeacherSchema teacher,
		IRepositoryExpression<Teacher> findDublicate,
		IRepositoryExpression<TeachersDepartment> findDepartment
	)
	{
		Teacher = teacher;
		FindDublicate = findDublicate;
		FindDepartment = findDepartment;
		Validator = new TeacherValidator()
		.WithNameValidation(teacher)
		.WithConditionValidation(teacher)
		.WithJobTitle(teacher);
		Validator.ProcessValidation();
	}
}
