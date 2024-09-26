namespace StudentPerfomance.Domain.Errors.EducationPlans;

public class EducationPlanWithoutDirectionError : Error
{
	public EducationPlanWithoutDirectionError(string error = "Не указано направление подготовки") : base(error) { }
}
