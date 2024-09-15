namespace StudentPerfomance.Application.Queries.Group.GetGroupsByPage;

internal sealed class GetGroupsByPageQuery(int page, int pageSize) : IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
}
