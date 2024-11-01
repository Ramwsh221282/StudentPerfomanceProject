using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Commands.AttachEducationPlan;

public class AttachEducationPlanCommand
(
	EducationPlan? plan,
	StudentGroup? group,
	byte? semesterNumber
) : ICommand<StudentGroup>
{
	public EducationPlan? Plan { get; init; } = plan;

	public StudentGroup? Group { get; init; } = group;

	public byte SemesterNumber = semesterNumber.GetValueOrDefault();
}
