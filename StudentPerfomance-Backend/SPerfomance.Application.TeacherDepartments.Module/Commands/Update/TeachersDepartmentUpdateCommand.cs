using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Update;

internal sealed class TeachersDepartmentUpdateCommand : ICommand
{
	private readonly DepartmentSchema _newSchema;
	private readonly IRepositoryExpression<TeachersDepartment> _findInitial;
	private readonly IRepositoryExpression<TeachersDepartment> _findNameDublicate;
	private readonly TeacherDepartmentsQueryRepository _repository;

	public readonly ICommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment> Handler;

	public TeachersDepartmentUpdateCommand(DepartmentSchema newSchema, DepartmentSchema oldSchema)
	{
		_newSchema = newSchema;
		_findInitial = ExpressionsFactory.GetDepartment(oldSchema.ToRepositoryObject());
		_findNameDublicate = ExpressionsFactory.GetDepartment(newSchema.ToRepositoryObject());
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(TeacherDepartmentsQueryRepository repository) : ICommandHandler<TeachersDepartmentUpdateCommand, TeachersDepartment>
	{
		private readonly TeacherDepartmentsQueryRepository _repository = repository;

		public async Task<OperationResult<TeachersDepartment>> Handle(TeachersDepartmentUpdateCommand command)
		{
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
