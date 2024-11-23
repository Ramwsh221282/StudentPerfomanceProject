using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.SemesterPlans;

namespace SPerfomance.Application.Semesters.DTO;

public class SemesterPlanDto
{
    public Guid Id { get; set; }
    public SemesterDto? Semester { get; set; }
    public string? Discipline { get; set; }
    public TeacherDto? Teacher { get; set; }
}

public static class SemesterPlanDtoExtension
{
    public static SemesterPlanDto MapFromDomain(this SemesterPlan plan)
    {
        var semesterDto = plan.Semester.MapFromDomain();
        return new SemesterPlanDto()
        {
            Id = plan.Id,
            Semester = semesterDto,
            Discipline = plan.Discipline.Name,
            Teacher = plan.Teacher == null ? null : plan.Teacher.MapFromDomain(),
        };
    }
}
