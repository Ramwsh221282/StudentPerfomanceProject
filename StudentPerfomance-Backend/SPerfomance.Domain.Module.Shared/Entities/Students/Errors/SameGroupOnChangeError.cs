using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Students.Errors;

public sealed class SameGroupOnChangeError : Error
{
	public SameGroupOnChangeError() => error = "Студент уже состоит в этой группе";
	public override string ToString() => error;
}
