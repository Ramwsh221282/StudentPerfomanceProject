namespace StudentPerfomance.Domain.Errors.Semesters;

public sealed class SemesterNumberError : Error
{
	public SemesterNumberError() => error = "Некорректный номер семестра";
}
