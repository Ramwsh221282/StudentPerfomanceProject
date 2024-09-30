using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGrades.Errors;

public sealed class GradeValueError : Error
{
	public GradeValueError() => error = "Только значения оценок 2-5 или \"н\a\" допустимы";
	public override string ToString() => error;
}
