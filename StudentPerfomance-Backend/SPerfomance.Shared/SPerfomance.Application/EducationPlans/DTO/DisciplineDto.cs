using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.SemesterPlans;

namespace SPerfomance.Application.EducationPlans.DTO;

public sealed class DisciplineDto
{
    public Guid Id { get; set; }
    public string? DisciplineName { get; set; }
    public TeacherDto? Teacher { get; set; }
}

public static class DisciplineDtoExtensions
{
    public static DisciplineDto MapFromDomain(this SemesterPlan plan) =>
        new()
        {
            Id = plan.Id,
            DisciplineName = plan.Discipline.Name,
            Teacher = plan.Teacher == null ? null : plan.Teacher.MapFromDomain(),
        };
}
