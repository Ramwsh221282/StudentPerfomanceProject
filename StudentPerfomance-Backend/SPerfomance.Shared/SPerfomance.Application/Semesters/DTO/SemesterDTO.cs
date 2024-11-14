using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Application.Semesters.DTO;

public class SemesterDTO
{
    public EducationPlanDTO? EducationPlan { get; set; }

    public byte? Number { get; set; }

    public int? ContractsCount { get; set; }
}

public static class SemesterPlanDTOExtensions
{
    public static SemesterDTO MapFromDomain(this Semester semester)
    {
        EducationPlanDTO planDTO = semester.Plan.MapFromDomain();
        SemesterDTO dto = new SemesterDTO()
        {
            EducationPlan = planDTO,
            Number = semester.Number.Number,
            ContractsCount = semester.Disciplines.Count,
        };
        return dto;
    }
}
