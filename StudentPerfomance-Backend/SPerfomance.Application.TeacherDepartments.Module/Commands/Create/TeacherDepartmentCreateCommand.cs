using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Departments.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Create;

internal sealed class TeacherDepartmentCreateCommand : ICommand
{
	public DepartmentSchema Department { get; init; }
	public IRepositoryExpression<TeachersDepartment> NameDublicate { get; init; }
	public ISchemaValidator Validator { get; init; }
	public TeacherDepartmentCreateCommand(DepartmentSchema department, IRepositoryExpression<TeachersDepartment> nameDublicate)
	{
		Department = department;
		NameDublicate = nameDublicate;
		Validator = new DepartmentSchemaValidator().WithNameValidation(department);
		Validator.ProcessValidation();
	}
}
