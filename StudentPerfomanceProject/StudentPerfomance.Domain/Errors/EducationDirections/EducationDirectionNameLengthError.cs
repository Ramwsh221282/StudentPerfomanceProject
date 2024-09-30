namespace StudentPerfomance.Domain.Errors.EducationDirections;

public class EducationDirectionNameLengthError : Error
{
	public EducationDirectionNameLengthError(int length) => error = $"Название направления подготовки превышает {length} символов";
}
