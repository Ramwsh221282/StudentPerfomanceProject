using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api.Requests;

public record EducationPlanFilterRequest(EducationPlanDTO Plan, int Page, int PageSize, string Token);
