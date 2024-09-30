namespace StudentPerfomance.Domain.Errors.SemesterPlans;

public class SemesterPlanError : Error
{
	public SemesterPlanError() => error = "Невозможно создать связку семестр + дисциплина";
}
