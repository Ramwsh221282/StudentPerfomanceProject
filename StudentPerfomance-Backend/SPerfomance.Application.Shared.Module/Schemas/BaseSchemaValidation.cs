using System.Text;

namespace SPerfomance.Application.Shared.Module.Schemas;

public abstract class BaseSchemaValidation
{
	protected readonly StringBuilder errorBuilder = new StringBuilder();
	protected void AppendErrorText(string message) => errorBuilder.AppendLine(message);
	protected abstract bool Validate();

	protected bool ReturnSuccess() => true;
	protected bool ReturnError(string message)
	{
		errorBuilder.Append(message);
		return false;
	}
}
