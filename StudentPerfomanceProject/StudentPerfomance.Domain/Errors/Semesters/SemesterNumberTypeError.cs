namespace StudentPerfomance.Domain.Errors.Semesters;

public sealed class SemesterNumberTypeError : Error
{
	public SemesterNumberTypeError() => error = "Недопустимое значение номера семестра";
}
