using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Application.Commands.Semesters.CreateSemester;

internal sealed class CreateSemesterCommandHandler
(
	IRepository<Semester> semesters,
	IRepository<StudentGroup> groups
)
: CommandWithErrorBuilder, ICommandHandler<CreateSemesterCommand, OperationResult<Semester>>
{
	private readonly IRepository<Semester> _semesters = semesters;
	private readonly IRepository<StudentGroup> _groups = groups;
	public async Task<OperationResult<Semester>> Handle(CreateSemesterCommand command)
	{
		command.Validator.ValidateSchema(this);
		await _semesters.ValidateExistance(command.Dublicate, "Такой семестр для группы уже существует", this);
		StudentGroup? group = await _groups.GetByParameter(command.Existance);
		group.ValidateNullability("Группа не найдена", this);
		return await this.ProcessAsync(async () =>
		{
			/*SemesterNumber number = SemesterNumber.Create(command.Semester.Number).Value;
			Semester semester = Semester.Create(Guid.NewGuid(), group, number).Value;
			await _semesters.Create(semester);
			return new OperationResult<Semester>(semester);*/
			return new OperationResult<Semester>("semester");
		});
	}
}
