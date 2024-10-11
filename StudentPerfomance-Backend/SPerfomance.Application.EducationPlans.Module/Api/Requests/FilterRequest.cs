using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api.Requests;

public record FilterRequest(EducationPlanSchema Plan, int Page, int PageSize);
