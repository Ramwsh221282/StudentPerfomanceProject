using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Semesters.Errors;

public static class SemesterErrors
{
	public static Error NotFound() => new Error("Семестр не найден");

	public static Error NullError() => new Error("Семестр не указан");

	public static Error NumberEmpty() => new Error("Номер семестра был пустым");
}
