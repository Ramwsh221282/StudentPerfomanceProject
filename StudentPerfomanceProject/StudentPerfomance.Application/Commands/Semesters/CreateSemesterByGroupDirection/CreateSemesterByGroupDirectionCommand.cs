using StudentPerfomance.Domain.Constants;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Application.Commands.Semesters.CreateSemesterByGroupDirection;

internal sealed class CreateSemesterByGroupDirectionCommand : ICommand
{
	public Semester[] Semesters { get; private set; } = null!;
	public OperationResult<StudentGroup> Result { get; init; }
	public CreateSemesterByGroupDirectionCommand(OperationResult<StudentGroup> result)
	{
		Result = result;
	}

	private void InitializeSemestersByGroup(OperationResult<StudentGroup> result)
	{
		/*if (result.Result != null)
		{
			Semesters = result.Result.EducationDirection.Direction switch
			{
				"Бакалавриат" => InitializeBachelorSemesters(result.Result),
				"Магистратура" => InitializeMagisterSemesters(result.Result),
				_ => InitializeEmpty(),
			};
		}*/
	}
}
