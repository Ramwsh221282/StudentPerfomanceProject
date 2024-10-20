using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;

internal sealed class TeacherDepartmentDeleteCommand : ICommand
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression;
	private readonly TeacherDepartmentsCommandRepository _repository;

	public readonly ICommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment> Handler;

	public TeacherDepartmentDeleteCommand(DepartmentSchema department, string token)
	{
		_expression = ExpressionsFactory.GetDepartment(department.ToRepositoryObject());
		_repository = new TeacherDepartmentsCommandRepository();
		Handler = new VerificationHandler<TeacherDepartmentDeleteCommand, TeachersDepartment>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment>
	{
		private readonly TeacherDepartmentsCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment> handler,
			TeacherDepartmentsCommandRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentDeleteCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<TeachersDepartment> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<TeachersDepartment>(delete.Error) :
				new OperationResult<TeachersDepartment>(delete.Value);
		}
	}
}
