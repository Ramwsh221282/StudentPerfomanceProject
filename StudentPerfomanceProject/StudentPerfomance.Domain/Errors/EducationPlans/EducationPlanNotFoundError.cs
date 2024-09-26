namespace StudentPerfomance.Domain.Errors.EducationPlans;

public sealed class EducationPlanNotFoundError : Error
{
	public EducationPlanNotFoundError(string error = "Учебный план не найден") : base(error) { }
}
