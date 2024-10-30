using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;

namespace SPerfomance.Application.Semesters.Commands.RemoveDiscipline;

public class RemoveDisciplineCommand(SemesterPlan? discipline) : ICommand<SemesterPlan>
{
	public SemesterPlan? Discipline { get; init; } = discipline;
}
