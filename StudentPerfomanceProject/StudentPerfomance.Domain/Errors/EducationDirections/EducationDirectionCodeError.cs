namespace StudentPerfomance.Domain.Errors.EducationDirections;

public class EducationDirectionCodeError : Error
{
	public EducationDirectionCodeError() => error = "Некорректный код направления";
}
