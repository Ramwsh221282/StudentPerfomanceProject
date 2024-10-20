using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Update;

internal sealed class TeachersDepartmentUpdateCommand : ICommand
{
	private readonly DepartmentSchema _newSchema;
	private readonly IRepositoryExpression<TeachersDepartment> _findInitial;
	private readonly IRepositoryExpression<TeachersDepartment> _findNameDublicate;
	private readonly TeacherDepartmentsQueryRepository _repository;

	public readonly ICommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment> Handler;

	public TeachersDepartmentUpdateCommand(DepartmentSchema newSchema, DepartmentSchema oldSchema, string token)
	{
		_newSchema = newSchema;
		_findInitial = ExpressionsFactory.GetDepartment(oldSchema.ToRepositoryObject());
		_findNameDublicate = ExpressionsFactory.GetDepartment(newSchema.ToRepositoryObject());
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new VerificationHandler<TeachersDepartmentUpdateCommand, TeachersDepartment>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal sealed class CommandHandler : DecoratedCommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment>
	{
		private readonly TeacherDepartmentsQueryRepository _repository;

		public CommandHandler(
			ICommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment> handler,
			TeacherDepartmentsQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<TeachersDepartment>> Handle(TeachersDepartmentUpdateCommand command)
		{
			var result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			TeachersDepartment? department = await _repository.GetByParameter(command._findInitial);
			if (department == null)
				return new OperationResult<TeachersDepartment>(new DepartmentNotFountError().ToString());

			if (await _repository.HasEqualRecord(command._findNameDublicate))
				return new OperationResult<TeachersDepartment>(new DepartmentNameDublicateError(command._newSchema.Name).ToString());

			department.ChangeDepartmentName(command._newSchema.Name);
			await _repository.Commit();
			return new OperationResult<TeachersDepartment>(department);
		}
	}
}
