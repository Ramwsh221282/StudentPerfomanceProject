using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Application.Semesters.DTO;

public class SemesterDto
{
    public Guid Id { get; set; }
    public EducationPlanDto? EducationPlan { get; set; }
    public byte? Number { get; set; }
    public int? ContractsCount { get; set; }
}

public static class SemesterPlanDtoExtensions
{
    public static SemesterDto MapFromDomain(this Semester semester)
    {
        var planDto = semester.Plan.MapFromDomain();
        var dto = new SemesterDto()
        {
            Id = semester.Id,
            EducationPlan = planDto,
            Number = semester.Number.Number,
            ContractsCount = semester.Disciplines.Count,
        };
        return dto;
    }
}
