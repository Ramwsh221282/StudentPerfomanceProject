using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;

namespace StudentPerfomance.Api.Requests.EducationPlanRequests;

public record EducationPlanGeneralRequest
{
	public EducationPlanSchema Plan { get; init; }
	public EducationPlanGeneralRequest(EducationPlanSchema plan)
	{
		Plan = plan;
	}
}
