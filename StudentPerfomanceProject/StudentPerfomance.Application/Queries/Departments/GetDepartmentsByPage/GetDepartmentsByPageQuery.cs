namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByPage;

internal sealed class GetDepartmentsByPageQuery(int page, int pageSize) : IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
}
