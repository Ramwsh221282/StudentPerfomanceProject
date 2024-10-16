using SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Api.Requests;

public record ChangeDisciplineNameRequest(SemesterPlanDTO? Initial, SemesterPlanDTO? Updated);
