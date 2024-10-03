using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Departments.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Create;

public sealed class TeacherDepartmentCreateCommand : ICommand
{
	private readonly DepartmentSchema _department;
	private readonly IRepositoryExpression<TeachersDepartment> _nameDublicate;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment> Handler;
	public TeacherDepartmentCreateCommand(DepartmentSchema department, IRepositoryExpression<TeachersDepartment> nameDublicate, IRepository<TeachersDepartment> repository)
	{
		_department = department;
		_nameDublicate = nameDublicate;
		_validator = new DepartmentSchemaValidator().WithNameValidation(department);
		_validator.ProcessValidation();
		Handler = new CommandHandler(repository);
	}

	internal sealed class CommandHandler(IRepository<TeachersDepartment> repository) : ICommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentCreateCommand command)
		{
			if (!command._validator.IsValid)
				return new OperationResult<TeachersDepartment>(command._validator.Error);
			if (await _repository.HasEqualRecord(command._nameDublicate))
				return new OperationResult<TeachersDepartment>(new DepartmentNameDublicateError(command._department.FullName).ToString());
			TeachersDepartment department = command._department.CreateDomainObject();
			await _repository.Create(department);
			return new OperationResult<TeachersDepartment>(department);
		}
	}
}
