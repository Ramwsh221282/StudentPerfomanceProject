using StudentPerfomance.Application.Commands;
using StudentPerfomance.Application.Queries;

namespace StudentPerfomance.Application;

internal static class OperationResultExtensions
{
	public static OperationResult<T> Process<T>(this CommandWithErrorBuilder builder, Func<OperationResult<T>> func)
	{
		if (builder.HasError) return new OperationResult<T>(builder.Error);
		return func.Invoke();
	}

	public static async Task<OperationResult<T>> ProcessAsync<T>(this CommandWithErrorBuilder builder, Func<Task<OperationResult<T>>> func)
	{
		if (builder.HasError) return new OperationResult<T>(builder.Error);
		return await func();
	}

	public static async Task<T> ProcessAsync<T>(this CommandWithErrorBuilder builder, Func<Task<T>> func) => await func();

	public static async Task<OperationResult<T>> ProcessAsync<TQuery, T>(this IQueryHandler<TQuery, OperationResult<T>> handler, Func<Task<OperationResult<T>>> func)
	where TQuery : IQuery => await func();
}
