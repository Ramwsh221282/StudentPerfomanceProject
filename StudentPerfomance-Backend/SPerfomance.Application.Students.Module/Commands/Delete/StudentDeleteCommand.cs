using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

namespace SPerfomance.Application.Students.Module.Commands.Delete;

public sealed class StudentDeleteCommand(IRepositoryExpression<Student> getStudent, IRepository<Student> repository) : ICommand
{
	private readonly IRepositoryExpression<Student> _getStudent = getStudent;
	public ICommandHandler<StudentDeleteCommand, Student> Handler { get; init; } = new CommandHandler(repository);
	internal sealed class CommandHandler(IRepository<Student> repository) : ICommandHandler<StudentDeleteCommand, Student>
	{
		private readonly IRepository<Student> _repository = repository;
		public async Task<OperationResult<Student>> Handle(StudentDeleteCommand command)
		{
			Student? student = await _repository.GetByParameter(command._getStudent);
			if (student == null)
				return new OperationResult<Student>(new StudentNotFoundError().ToString());
			return new OperationResult<Student>(student);
		}
	}
}
