using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.DeattachEducationPlan;

public class DeattachEducationPlanCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<DeattachEducationPlanCommand, StudentGroup>
{
    public async Task<Result<StudentGroup>> Handle(
        DeattachEducationPlanCommand command,
        CancellationToken ct = default
    )
    {
        if (command.Group == null)
            return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

        if (command.Group.EducationPlan == null)
            return Result<StudentGroup>.Success(command.Group);

        var group = command.Group.EducationPlan.RemoveStudentGroup(command.Group);
        if (!group.IsFailure)
            await repository.DeattachEducationPlanId(group.Value, ct);

        return group;
    }
}
