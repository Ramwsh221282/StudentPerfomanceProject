namespace StudentPerfomance.Domain.Errors.EducationDirections;

public sealed class EducationDirectionTypeError : Error
{
	public EducationDirectionTypeError() => error = "Тип направления некорректен";
}
