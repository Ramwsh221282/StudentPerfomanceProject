using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;

namespace SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;

public interface IService<TResult>
{
	Task<OperationResult<TResult>> DoOperation();
}
