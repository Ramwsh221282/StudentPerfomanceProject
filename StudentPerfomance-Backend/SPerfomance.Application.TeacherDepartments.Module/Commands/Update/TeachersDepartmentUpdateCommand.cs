using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Departments.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Update;

public sealed class TeachersDepartmentUpdateCommand : ICommand
{
	private readonly DepartmentSchema _newSchema;
	private readonly IRepositoryExpression<TeachersDepartment> _findInitial;
	private readonly IRepositoryExpression<TeachersDepartment> _findNameDublicate;
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment> Handler;
	public TeachersDepartmentUpdateCommand
	(
		DepartmentSchema newSchema,
		IRepositoryExpression<TeachersDepartment> findInitial,
		IRepositoryExpression<TeachersDepartment> findNameDublicate,
		IRepository<TeachersDepartment> repository
	)
	{
		_newSchema = newSchema;
		_findInitial = findInitial;
		_findNameDublicate = findNameDublicate;
		_validator = new DepartmentSchemaValidator()
		.WithNameValidation(newSchema);
		_validator.ProcessValidation();
		Handler = new CommandHandler(repository);
	}

	internal sealed class CommandHandler(IRepository<TeachersDepartment> repository) : ICommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<TeachersDepartment>> Handle(TeachersDepartmentUpdateCommand command)
		{
			TeachersDepartment? department = await _repository.GetByParameter(command._findInitial);
			if (department == null)
				return new OperationResult<TeachersDepartment>(new DepartmentNotFountError().ToString());
			if (await _repository.HasEqualRecord(command._findNameDublicate))
				return new OperationResult<TeachersDepartment>(new DepartmentNameDublicateError(command._newSchema.FullName).ToString());
			department.ChangeDepartmentName(command._newSchema.FullName);
			await _repository.Commit();
			return new OperationResult<TeachersDepartment>(department);
		}
	}
}
