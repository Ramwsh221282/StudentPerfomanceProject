namespace StudentPerfomance.Application.Queries.Semesters.GetSemestersByPage;

internal sealed class GetSemestersByPageQuery(int page, int pageSize) : IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
}
