using StudentPerfomance.Application.Commands.Group.UpdateStudentGroup;
using StudentPerfomance.Domain.Constants;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.CompressSemestersOnGroupDirectionChange;

internal sealed class CompressSemestersOnGroupDirectionChangeCommandHandler
(
	IRepository<Semester> semesters
) : CommandWithErrorBuilder, ICommandHandler<CompressSemestersOnGroupDirectionChangeCommand, StudentGroup>
{
	private readonly IRepository<Semester> _semesters = semesters;
	public async Task<StudentGroup> Handle(CompressSemestersOnGroupDirectionChangeCommand command)
	{
		return await this.ProcessAsync(async () =>
		{
			await UpdateSemestersByGroup(_semesters, command.Group, command);
			return command.Group;
		});
	}

	private async Task UpdateSemestersByGroup
	(
		IRepository<Semester> repository,
		StudentGroup group,
		CompressSemestersOnGroupDirectionChangeCommand command
	)
	{
		/*Task operation = group.EducationDirection.Direction switch
		{
			StudentGroupEducationDirectionNames.Magister => new StudentGroupSemestersCompressStrategy
			(
				repository,
				group,
				StudentGroupEducationDirectionNames.MagisterSemestersCount
			).Process(),
			StudentGroupEducationDirectionNames.Bachelor => new StudentGroupSemestersExtendStrategy
			(
				repository,
				command.ByGroup,
				group,
				StudentGroupEducationDirectionNames.BachelorSemestersCount)
			.Process(),
			_ => Task.Run(() => { }),
		};
		await operation;*/
	}
}
