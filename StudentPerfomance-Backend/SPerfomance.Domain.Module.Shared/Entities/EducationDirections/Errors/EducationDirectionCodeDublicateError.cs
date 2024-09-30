using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

public sealed class EducationDirectionCodeDublicateError : Error
{
	public EducationDirectionCodeDublicateError(string code) => error = $"Направление подготовки с кодом {code} уже есть в системе";
	public override string ToString() => error;
}
