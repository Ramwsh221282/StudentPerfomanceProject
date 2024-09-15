namespace StudentPerfomance.Application;

public interface IService<TResult>
{
	Task<OperationResult<TResult>> DoOperation();
}
