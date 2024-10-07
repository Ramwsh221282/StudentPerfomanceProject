namespace SPerfomance.Application.Shared.Module.Operations;

public class OperationResult<TResult>
{
	private TResult? _result;
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

	public OperationResult()
	{
		_isFailed = false;
		_errorMessage = string.Empty;
	}

	public OperationResult<TResult> SetNotFailedFlag()
	{
		_isFailed = false;
		return this;
	}

	public OperationResult<TResult> SetFailedFlag()
	{
		_isFailed = true;
		return this;
	}

	public OperationResult<TResult> SetResult(TResult result)
	{
		_result = result;
		return this;
	}

	public TResult? Result => _result;
	public bool IsFailed => _isFailed;
	public string Error => _errorMessage;
}
