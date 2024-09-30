namespace StudentPerfomance.Domain.Errors.EducationDirections;

public sealed class EducationDirectionNameError : Error
{
	public EducationDirectionNameError() => error = "Некорректное название направления подготовки";
}
