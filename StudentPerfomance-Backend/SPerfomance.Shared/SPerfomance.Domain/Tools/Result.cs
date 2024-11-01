namespace SPerfomance.Domain.Tools;

public sealed class Result<TValue> where TValue : class
{
	private Result(Error error)
	{
		Error = error;
		IsSuccess = false;
		IsFailure = true;
	}

	private Result(TValue value)
	{
		Error = Error.None;
		IsSuccess = true;
		IsFailure = false;
		Value = value;
	}

	public TValue Value { get; init; } = null!;

	public bool IsSuccess { get; init; }

	public bool IsFailure { get; init; }

	public Error Error { get; init; } = Error.None;

	public static Result<TValue> Success(TValue value) => new Result<TValue>(value);

	public static Result<TValue> Failure(Error error) => new Result<TValue>(error);

	public static implicit operator Result<TValue>(TValue value) => new Result<TValue>(value);

	public static implicit operator Result<TValue>(Error error) => new Result<TValue>(error);
}
