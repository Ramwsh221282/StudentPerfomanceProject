namespace StudentPerfomance.Domain.Errors.EducationPlans;

public sealed class EducationPlanIncorrectYearError : Error
{
	public EducationPlanIncorrectYearError(string error = "Недопустимое значение года направления") : base(error) { }
}
