using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.StudentGroups.Commands.DeattachEducationPlan;

public class DeattachEducationPlanCommandHandler(IStudentGroupsRepository repository)
    : ICommandHandler<DeattachEducationPlanCommand, StudentGroup>
{
    private readonly IStudentGroupsRepository _repository = repository;

    public async Task<Result<StudentGroup>> Handle(DeattachEducationPlanCommand command)
    {
        if (command.Group == null)
            return Result<StudentGroup>.Failure(StudentGroupErrors.NotFound());

        if (command.Group.EducationPlan == null)
            return Result<StudentGroup>.Success(command.Group);

        Result<StudentGroup> group = command.Group.EducationPlan.RemoveStudentGroup(command.Group);
        if (!group.IsFailure)
            await _repository.DeattachEducationPlanId(group.Value);

        return group;
    }
}
