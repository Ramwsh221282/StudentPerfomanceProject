using System.Text;

namespace StudentPerfomance.Application.Commands;

internal abstract class CommandWithErrorBuilder
{
	private readonly StringBuilder _errorBuilder = new StringBuilder();
	public void AppendError(string message) => _errorBuilder.AppendLine(message);
	public bool HasError => _errorBuilder.Length > 0;
	public string Error => _errorBuilder.ToString();
}
