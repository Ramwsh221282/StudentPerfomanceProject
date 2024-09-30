using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Disciplines.Errors;

public sealed class DisciplineNameError : Error
{
	public DisciplineNameError() => error = "Название дисциплины пустое";
	public override string ToString() => error;
}
