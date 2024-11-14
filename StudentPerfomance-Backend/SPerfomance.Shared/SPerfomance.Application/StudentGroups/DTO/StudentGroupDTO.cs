using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.DTO;

public class StudentGroupDTO
{
    public int EntityNumber { get; init; }

    public string Name { get; init; }

    public EducationPlanDTO? Plan { get; init; }

    public byte? ActiveSemesterNumber { get; init; }

    public StudentGroupDTO(StudentGroup group)
    {
        EntityNumber = group.EntityNumber;
        Name = group.Name.Name;
        Plan = group.EducationPlan == null ? null : group.EducationPlan.MapFromDomain();
        ActiveSemesterNumber =
            group.ActiveGroupSemester == null ? null : group.ActiveGroupSemester.Number.Number;
    }
}

public static class StudentGroupDTOExtensions
{
    public static StudentGroupDTO MapFromDomain(this StudentGroup group)
    {
        return new StudentGroupDTO(group);
    }
}
