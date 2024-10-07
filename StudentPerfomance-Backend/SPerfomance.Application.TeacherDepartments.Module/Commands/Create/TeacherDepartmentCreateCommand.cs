using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Create;

internal sealed class TeacherDepartmentCreateCommand : ICommand
{
	private readonly DepartmentSchema _department;
	private readonly IRepositoryExpression<TeachersDepartment> _nameDublicate;
	private readonly TeacherDepartmentsCommandRepository _repository;
	public readonly ICommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment> Handler;

	public TeacherDepartmentCreateCommand(DepartmentSchema department)
	{
		_department = department;
		_repository = new TeacherDepartmentsCommandRepository();
		Handler = new CommandHandler(_repository);
		_nameDublicate = ExpressionsFactory.GetByName(department.ToRepositoryObject());
	}

	internal sealed class CommandHandler(TeacherDepartmentsCommandRepository repository) : ICommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment>
	{
		private readonly TeacherDepartmentsCommandRepository _repository = repository;
		public async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentCreateCommand command)
		{

			Result<TeachersDepartment> create = await _repository.Create(command._department, command._nameDublicate);
			return create.IsFailure ?
				new OperationResult<TeachersDepartment>(create.Error) :
				new OperationResult<TeachersDepartment>(create.Value);
		}
	}
}
