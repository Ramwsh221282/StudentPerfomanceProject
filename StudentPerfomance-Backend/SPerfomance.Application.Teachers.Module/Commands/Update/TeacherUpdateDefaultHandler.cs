using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Application.Teachers.Module.Commands.Update;

public sealed class TeacherUpdateDefaultHandler(IRepository<Teacher> repository) : ICommandHandler<TeacherUpdateCommand, OperationResult<Teacher>>
{
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<Teacher>> Handle(TeacherUpdateCommand command)
	{
		if (!command.Validator.IsValid)
			return new OperationResult<Teacher>(command.Validator.Error);
		if (await _repository.HasEqualRecord(command.FindDublicate))
			return new OperationResult<Teacher>(new TeacherDublicateError
			(
				command.NewSchema.Name,
				command.NewSchema.Surname,
				command.NewSchema.Thirdname,
				command.NewSchema.Job,
				command.NewSchema.Condition
			).ToString());
		Teacher? teacher = await _repository.GetByParameter(command.FindInitial);
		if (teacher == null) return new OperationResult<Teacher>(new TeacherNotFoundError().ToString());
		return new OperationResult<Teacher>(teacher);
	}
}
