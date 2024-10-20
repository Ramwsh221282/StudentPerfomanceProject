using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api.Requests;

public record EducationPlanSearchRequest(EducationPlanDTO Plan, string Token);
