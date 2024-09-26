namespace StudentPerfomance.Domain.Errors;

public abstract class Error
{
	protected readonly string error;
	public Error(string error) => this.error = error;
	public override string ToString() => error;
}
