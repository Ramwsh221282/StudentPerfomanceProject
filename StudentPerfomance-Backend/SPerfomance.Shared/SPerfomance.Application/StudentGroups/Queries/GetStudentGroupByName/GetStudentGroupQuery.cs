using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;

public class GetStudentGroupQuery(string? name) : IQuery<StudentGroup>
{
    public string? Name { get; init; } = name;
}
