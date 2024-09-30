using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class EducationDirectionNotFoundError : Error
{
	public EducationDirectionNotFoundError() => error = "Направление подготовки не найдено";
	public override string ToString() => error;
}
