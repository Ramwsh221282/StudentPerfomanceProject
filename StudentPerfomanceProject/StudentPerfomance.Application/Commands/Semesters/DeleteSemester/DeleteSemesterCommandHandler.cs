using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.DeleteSemester;

internal sealed class DeleteSemesterCommandHandler
(
	IRepository<Semester> repository
)
: CommandWithErrorBuilder, ICommandHandler<DeleteSemesterCommand, OperationResult<Semester>>
{
	private readonly IRepository<Semester> _repository = repository;
	public async Task<OperationResult<Semester>> Handle(DeleteSemesterCommand command)
	{
		Semester? semester = await _repository.GetByParameter(command.Existance);
		semester.ValidateNullability("Семестр не найден", this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(semester);
			return new OperationResult<Semester>(semester);
		});
	}
}
