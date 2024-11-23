using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.DTO;

public class StudentGroupDto(StudentGroup group)
{
    public Guid Id { get; set; } = group.Id;
    public int EntityNumber { get; init; } = group.EntityNumber;
    public string Name { get; init; } = group.Name.Name;
    public EducationPlanDto? Plan { get; init; } =
        group.EducationPlan == null ? null : group.EducationPlan.MapFromDomain();
    public byte? ActiveSemesterNumber { get; init; } =
        group.ActiveGroupSemester == null ? null : group.ActiveGroupSemester.Number.Number;
}

public static class StudentGroupDtoExtensions
{
    public static StudentGroupDto MapFromDomain(this StudentGroup group)
    {
        return new StudentGroupDto(group);
    }

    public static byte EstimateCourse(this StudentGroupDto group)
    {
        if (group.ActiveSemesterNumber == null)
            return default;
        if (group.Plan?.Direction == null)
            return default;

        return group.Plan.Direction.Type == "Бакалавриат"
            ? group.EstimateCourseFromBachelor()
            : group.EstimateCourseFromMagister();
    }

    private static byte EstimateCourseFromBachelor(this StudentGroupDto group)
    {
        if (group.ActiveSemesterNumber == null)
            return default;

        if (group.Plan?.Direction == null)
            return default;

        if (group.Plan.Direction.Type != "Бакалавриат")
            return default;

        return group.ActiveSemesterNumber switch
        {
            1 => 1,
            2 => 1,
            3 => 2,
            4 => 2,
            5 => 3,
            6 => 3,
            7 => 4,
            8 => 4,
            _ => default,
        };
    }

    private static byte EstimateCourseFromMagister(this StudentGroupDto group)
    {
        if (group.ActiveSemesterNumber == null)
            return default;

        if (group.Plan?.Direction == null)
            return default;

        if (group.Plan.Direction.Type != "Магистратура")
            return default;

        return group.ActiveSemesterNumber switch
        {
            1 => 1,
            2 => 1,
            3 => 2,
            4 => 2,
            _ => default,
        };
    }
}
