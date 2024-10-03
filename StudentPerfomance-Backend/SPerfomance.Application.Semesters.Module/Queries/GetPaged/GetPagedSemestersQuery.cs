using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Semesters.Module.Queries.GetPaged;

public sealed class GetPagedSemestersQuery(int page, int pageSize, IRepository<Semester> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public IQueryHandler<GetPagedSemestersQuery, IReadOnlyCollection<Semester>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<Semester> repository) : IQueryHandler<GetPagedSemestersQuery, IReadOnlyCollection<Semester>>
	{
		private readonly IRepository<Semester> _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Semester>>> Handle(GetPagedSemestersQuery query)
		{
			IReadOnlyCollection<Semester> semesters = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Semester>>(semesters);
		}
	}
}
