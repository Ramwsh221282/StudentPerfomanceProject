namespace StudentPerfomance.Application.Queries.Student.GetStudentsByFilter;

internal sealed class GetStudentsByFilterQuery(int page, int pageSize) : IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
}
