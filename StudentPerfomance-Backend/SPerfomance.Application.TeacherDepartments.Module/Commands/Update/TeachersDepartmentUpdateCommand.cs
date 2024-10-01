using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Departments.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Update;

internal sealed class TeachersDepartmentUpdateCommand : ICommand
{
	public DepartmentSchema NewSchema { get; init; }
	public IRepositoryExpression<TeachersDepartment> FindInitial { get; init; }
	public IRepositoryExpression<TeachersDepartment> FindNameDublicate { get; init; }
	public ISchemaValidator Validator { get; init; }

	public TeachersDepartmentUpdateCommand
	(
		DepartmentSchema newSchema,
		IRepositoryExpression<TeachersDepartment> findInitial,
		IRepositoryExpression<TeachersDepartment> findNameDublicate
	)
	{
		NewSchema = newSchema;
		FindInitial = findInitial;
		FindNameDublicate = findNameDublicate;
		Validator = new DepartmentSchemaValidator()
		.WithNameValidation(newSchema);
		Validator.ProcessValidation();
	}
}
