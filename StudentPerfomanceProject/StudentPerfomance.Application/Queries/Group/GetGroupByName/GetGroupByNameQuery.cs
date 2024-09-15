using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Application.Queries.Group.GetGroupByName;

internal sealed class GetGroupByNameQuery(StudentsGroupSchema group) : IQuery
{
	public StudentsGroupSchema Group { get; init; } = group;
}
