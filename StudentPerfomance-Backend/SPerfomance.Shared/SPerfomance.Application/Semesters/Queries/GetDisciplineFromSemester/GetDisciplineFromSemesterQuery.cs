using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;

public class GetDisciplineFromSemesterQuery(Semester? semester, string? name) : IQuery<SemesterPlan>
{
	public Semester? Semester { get; init; } = semester;

	public string Name { get; init; } = name.ValueOrEmpty();
}
