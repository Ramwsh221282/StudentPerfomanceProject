using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.AttachEducationPlan;

public class AttachEducationPlanCommandHandler
(
	IStudentGroupsRepository repository
) : ICommandHandler<AttachEducationPlanCommand, StudentGroup>
{
	private readonly IStudentGroupsRepository _repository = repository;

	public async Task<Result<StudentGroup>> Handle(AttachEducationPlanCommand command)
	{
		if (command.Group == null)
			return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

		if (command.Plan == null)
			return Result<StudentGroup>.Failure(EducationPlanErrors.NotFoundError());

		Result<StudentGroup> group = command.Plan.AddStudentGroup(command.Group, command.SemesterNumber);
		if (!group.IsFailure)
			await _repository.AttachEducationPlanId(group.Value);

		return group;
	}
}
