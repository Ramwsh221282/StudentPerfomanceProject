using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsByFilter;

internal sealed class GetGroupsByFilterQuery(StudentsGroupSchema group, int page, int pageSize) : IQuery
{
	public StudentsGroupSchema Group { get; init; } = group;
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
}
