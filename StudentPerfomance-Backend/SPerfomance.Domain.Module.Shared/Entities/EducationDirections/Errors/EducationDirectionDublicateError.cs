using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class EducationDirectionDublicateError : Error
{
	public EducationDirectionDublicateError() => error = "Невозможно создать дубликат направления подготовки";
	public override string ToString() => error;
}
