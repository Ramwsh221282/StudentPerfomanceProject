using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Abstractions;

public interface IQueryHandler<TQuery, TResult>
	where TQuery : IQuery<TResult>
	where TResult : class
{
	Task<Result<TResult>> Handle(TQuery command);
}
