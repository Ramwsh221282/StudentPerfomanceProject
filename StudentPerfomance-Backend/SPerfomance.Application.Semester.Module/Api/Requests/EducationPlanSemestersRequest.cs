using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;

namespace SPerfomance.Application.Semester.Module.Api.Requests;

public record EducationPlanSemestersRequest(EducationPlanDTO? Plan, string Token);
