using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.SemesterPlans;

namespace SPerfomance.Application.Semesters.DTO;

public class SemesterPlanDTO
{
    public SemesterDTO? Semester { get; set; }

    public string? Discipline { get; set; }

    public TeacherDTO? Teacher { get; set; }
}

public static class SemesterPlanDTOExtension
{
    public static SemesterPlanDTO MapFromDomain(this SemesterPlan plan)
    {
        SemesterDTO semesterDTO = plan.Semester.MapFromDomain();
        return new SemesterPlanDTO()
        {
            Semester = semesterDTO,
            Discipline = plan.Discipline.Name,
            Teacher = plan.Teacher == null ? null : plan.Teacher.MapFromDomain(),
        };
    }
}
