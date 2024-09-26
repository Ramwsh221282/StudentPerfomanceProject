namespace StudentPerfomance.Domain.Errors.EducationPlans;

public sealed class EducationPlanDublicateError : Error
{
	public EducationPlanDublicateError(string error = "Учебный план уже существует") : base(error) { }
}
