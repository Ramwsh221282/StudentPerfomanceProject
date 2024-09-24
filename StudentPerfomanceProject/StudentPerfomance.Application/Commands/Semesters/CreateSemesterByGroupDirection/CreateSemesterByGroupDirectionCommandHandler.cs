using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.CreateSemesterByGroupDirection;

internal sealed class CreateSemesterByGroupDirectionCommandHandler
(
	IRepository<Semester> semesters
) : CommandWithErrorBuilder, ICommandHandler<CreateSemesterByGroupDirectionCommand, OperationResult<StudentGroup>>
{
	private readonly IRepository<Semester> _semesters = semesters;

	public async Task<OperationResult<StudentGroup>> Handle(CreateSemesterByGroupDirectionCommand command)
	{
		if (command.Result.IsFailed) AppendError(command.Result.Error);
		return await this.ProcessAsync(async () =>
		{
			/*foreach (var semester in command.Semesters)
			{
				await command.Result.Result.AddSemester(_semesters, semester);
			}*/
			return new OperationResult<StudentGroup>(command.Semesters.First().Group);
		});
	}
}
