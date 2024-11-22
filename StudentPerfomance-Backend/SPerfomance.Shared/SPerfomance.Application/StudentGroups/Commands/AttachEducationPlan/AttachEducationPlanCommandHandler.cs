using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.AttachEducationPlan;

public class AttachEducationPlanCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<AttachEducationPlanCommand, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        AttachEducationPlanCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Group == null)
            return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

        if (command.Plan == null)
            return Result<StudentGroup>.Failure(EducationPlanErrors.NotFoundError());

        var group = command.Plan.AddStudentGroup(command.Group, command.SemesterNumber);

        if (!group.IsFailure)
            await repository.AttachEducationPlanId(group.Value, ct);

        return group;
    }
}
