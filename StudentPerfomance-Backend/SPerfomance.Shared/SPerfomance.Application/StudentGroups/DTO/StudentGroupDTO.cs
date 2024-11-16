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

    public static byte EstimateCourse(this StudentGroupDTO group)
    {
        if (group.ActiveSemesterNumber == null)
            return default;
        if (group.Plan?.Direction == null)
            return default;

        return group.Plan.Direction.Type == "Бакалавриат"
            ? group.EstimateCourseFromBachelor()
            : group.EstimateCourseFromMagister();
    }

    private static byte EstimateCourseFromBachelor(this StudentGroupDTO group)
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

    private static byte EstimateCourseFromMagister(this StudentGroupDTO group)
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
