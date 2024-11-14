using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;

namespace SPerfomance.Api.Features.StudentGroups.Contracts;

public class StudentGroupContract
{
    public string? Name { get; set; }

    public static implicit operator GetStudentGroupQuery(StudentGroupContract contract) =>
        new GetStudentGroupQuery(contract.Name);
}
