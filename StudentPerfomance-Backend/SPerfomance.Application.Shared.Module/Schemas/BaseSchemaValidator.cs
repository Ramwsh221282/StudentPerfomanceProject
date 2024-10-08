using System.Text;

namespace SPerfomance.Application.Shared.Module.Schemas;

public abstract class BaseSchemaValidator
{
	protected readonly StringBuilder errorBuilder = new StringBuilder();
	protected bool isValid;
	protected void AppendError(string message)
	{
		if (errorBuilder.Length >= 1)
			errorBuilder.AppendLine(message);
		else
			errorBuilder.Append(message);
	}
}
