using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;

public class GetStudentFromGroupQuery
(
	StudentGroup? group,
	string? name,
	string? surname,
	string? patronymic,
	string? state,
	ulong? recordBook
) : IQuery<Student>
{
	public StudentGroup? Group { get; init; } = group;

	public string? Name { get; init; } = name;

	public string? Surname { get; init; } = surname;

	public string? Patronymic { get; init; } = patronymic;

	public string? State { get; init; } = state;

	public ulong? Recordbook { get; init; } = recordBook;
}
