using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;

public sealed class TeacherDepartmentDeleteCommand(IRepositoryExpression<TeachersDepartment> expression, IRepository<TeachersDepartment> repository) : ICommand
{
	private readonly IRepositoryExpression<TeachersDepartment> _expression = expression;
	public readonly ICommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment> Handler = new CommandHandler(repository);
	internal sealed class CommandHandler(IRepository<TeachersDepartment> repository) : ICommandHandler<TeacherDepartmentDeleteCommand, TeachersDepartment>
	{
		private readonly IRepository<TeachersDepartment> _repository = repository;
		public async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentDeleteCommand command)
		{
			TeachersDepartment? department = await _repository.GetByParameter(command._expression);
			if (department == null)
				return new OperationResult<TeachersDepartment>(new DepartmentNotFountError().ToString());
			await _repository.Remove(department);
			return new OperationResult<TeachersDepartment>(department);
		}
	}
}
