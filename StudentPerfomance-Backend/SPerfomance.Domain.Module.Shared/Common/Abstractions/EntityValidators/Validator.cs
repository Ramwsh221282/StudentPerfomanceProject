using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;

internal abstract class Validator<T> where T : class
{
	protected Error error;
	public Validator() => error = new DefaultError();
	public abstract bool Validate();
	public string GetErrorText() => error.ToString();
	protected bool HasError => error.ToString().Length == 0;
}
