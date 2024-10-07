using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Tests.Module;

internal sealed class OperationResultLogger<TOPeration, TResult>(TOPeration result, string title) where TOPeration : OperationResult<TResult>
{
	private readonly TOPeration _result = result;
	private readonly string _title = title;

	public void ShowInfo()
	{
		Console.WriteLine($"Operation title: {_title}");
		if (_result.IsFailed)
		{
			Console.WriteLine("Operation is failed");
			Console.WriteLine($"Failure flag: {_result.IsFailed}");
			Console.WriteLine($"Error message empty? {string.IsNullOrWhiteSpace(_result.Error)}");
			if (!string.IsNullOrWhiteSpace(_result.Error))
			{
				Console.WriteLine($"Error message: {_result.Error}");
			}
			return;
		}
		Console.WriteLine("Operation is success");
		Console.WriteLine($"Failure flag: {_result.IsFailed}");
		Console.WriteLine($"Error message empty? {string.IsNullOrWhiteSpace(_result.Error)}");
	}
}
