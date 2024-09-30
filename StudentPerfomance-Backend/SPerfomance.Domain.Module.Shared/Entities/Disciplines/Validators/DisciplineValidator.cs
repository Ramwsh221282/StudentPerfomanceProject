using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Disciplines.Validators;

internal sealed class DisciplineValidator : Validator<Discipline>
{
	private const int MAX_NAME_LENGTH = 50;
	private readonly string _name;

	public DisciplineValidator(string name) =>
		_name = name;

	public override bool Validate()
	{
		if (string.IsNullOrWhiteSpace(_name))
			error.AppendError(new DisciplineNameError());
		if (!string.IsNullOrWhiteSpace(_name) && _name.Length > MAX_NAME_LENGTH)
			error.AppendError(new DisciplineNameLengthError(MAX_NAME_LENGTH));
		return HasError;
	}
}
