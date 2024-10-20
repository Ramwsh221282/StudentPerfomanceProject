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

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Create;

internal sealed class TeacherDepartmentCreateCommand : ICommand
{
	private readonly DepartmentSchema _department;
	private readonly IRepositoryExpression<TeachersDepartment> _nameDublicate;
	private readonly TeacherDepartmentsCommandRepository _repository;

	public readonly ICommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment> Handler;

	public TeacherDepartmentCreateCommand(DepartmentSchema department, string token)
	{
		_department = department;
		_repository = new TeacherDepartmentsCommandRepository();
		Handler = new VerificationHandler<TeacherDepartmentCreateCommand, TeachersDepartment>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
		_nameDublicate = ExpressionsFactory.GetDepartment(department.ToRepositoryObject());
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment>
	{
		private readonly TeacherDepartmentsCommandRepository _repository;

		public CommandHandler(
			ICommandHandler<TeacherDepartmentCreateCommand, TeachersDepartment> handler,
			TeacherDepartmentsCommandRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentCreateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<TeachersDepartment> create = await _repository.Create(command._department, command._nameDublicate);
			return create.IsFailure ?
				new OperationResult<TeachersDepartment>(create.Error) :
				new OperationResult<TeachersDepartment>(create.Value);
		}
	}
}
