using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;

internal sealed class TeacherDepartmentDeleteCommand : ICommand
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression;
	private readonly TeacherDepartmentsCommandRepository _repository;
	public readonly ICommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment> Handler;

	public TeacherDepartmentDeleteCommand(DepartmentSchema department)
	{
		_expression = ExpressionsFactory.GetDepartment(department.ToRepositoryObject());
		_repository = new TeacherDepartmentsCommandRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(TeacherDepartmentsCommandRepository repository) : ICommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment>
	{
		private readonly TeacherDepartmentsCommandRepository _repository = repository;
		public async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentDeleteCommand command)
		{
			Result<TeachersDepartment> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<TeachersDepartment>(delete.Error) :
				new OperationResult<TeachersDepartment>(delete.Value);
		}
	}
}
