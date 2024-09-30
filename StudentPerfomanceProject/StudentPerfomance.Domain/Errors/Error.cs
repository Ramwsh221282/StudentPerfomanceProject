using System.Text;

namespace StudentPerfomance.Domain.Errors;

public abstract class Error
{
	protected string error;
	private readonly StringBuilder _errorBuilder;
	public Error()
	{
		_errorBuilder = new StringBuilder();
		error = string.Empty;
	}
	public override string ToString() => _errorBuilder.ToString();
	public void AppendError(Error error) => _errorBuilder.AppendLine(error.error);
}
