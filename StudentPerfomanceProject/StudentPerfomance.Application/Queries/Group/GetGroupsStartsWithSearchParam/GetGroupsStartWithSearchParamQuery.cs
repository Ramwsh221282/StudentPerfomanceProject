using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsStartsWithSearchParam;

internal sealed class GetGroupsStartWithSearchParamQuery(StudentsGroupSchema group) : IQuery
{
	public StudentsGroupSchema Group { get; init; } = group;
}
