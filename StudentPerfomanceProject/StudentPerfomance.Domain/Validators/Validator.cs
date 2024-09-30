using StudentPerfomance.Domain.Errors;

namespace StudentPerfomance.Domain.Validators;

internal abstract class Validator<T> where T : class
{
	protected Error error;
	public Validator() => error = new DefaultError();
	public abstract bool Validate();
	public string GetErrorText() => error.ToString();
}
