using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class CantRegisterAsTeacherError : Error
{
	public CantRegisterAsTeacherError() => error = "Модель преподавателя не найдена для этой учетной записи";

	public override string ToString() => error;
}
