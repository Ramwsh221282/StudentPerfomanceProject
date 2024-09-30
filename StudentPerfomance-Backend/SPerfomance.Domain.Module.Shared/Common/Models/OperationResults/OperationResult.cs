namespace SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

public class OperationResult<TResult>
{
	private readonly TResult? _result;
	private readonly string _errorMessage;
	private bool _isFailed;

	public OperationResult(TResult result)
	{
		_result = result;
		_isFailed = false;
		_errorMessage = string.Empty;
	}
	public OperationResult(string errorMessage)
	{
		_isFailed = true;
		_errorMessage = errorMessage;
	}

	public TResult? Result => _result;
	public bool IsFailed => _isFailed;
	public string Error => _errorMessage;
}
