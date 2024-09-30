namespace StudentPerfomance.Domain.Errors.EducationDirections;

public sealed class EducationDirectionCodeLengthError : Error
{
	public EducationDirectionCodeLengthError(int length) => error = $"Недопустимая длина кода направления. Максимально {length} символов";
}
