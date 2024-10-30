using System.Text;

namespace SPerfomance.Domain.Abstractions;

public abstract class AbstractSpecification
{
	protected StringBuilder ErrorBuilder = new StringBuilder();

	public void AppendError(string error) => ErrorBuilder.AppendLine(error);

	public string GetError()
	{
		string error = ErrorBuilder.ToString();
		ErrorBuilder = new StringBuilder();
		return error;
	}

	public bool HasError() => !(ErrorBuilder.Length > 0);
}
