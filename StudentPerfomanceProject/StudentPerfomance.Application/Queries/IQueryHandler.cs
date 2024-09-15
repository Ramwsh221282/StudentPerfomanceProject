using StudentPerfomance.Application.Queries;

namespace StudentPerfomance.Application;

internal interface IQueryHandler<TQuery, TQueryResult>
where TQuery : IQuery
{
	Task<TQueryResult> Handle(TQuery query);
}
