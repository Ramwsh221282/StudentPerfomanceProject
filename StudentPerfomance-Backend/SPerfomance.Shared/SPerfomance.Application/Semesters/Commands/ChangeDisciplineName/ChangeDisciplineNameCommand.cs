using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.ChangeDisciplineName;

public class ChangeDisciplineNameCommand(SemesterPlan? discipline, string? newName)
    : ICommand<SemesterPlan>
{
    public SemesterPlan? Discipline { get; init; } = discipline;

    public string NewName { get; init; } = newName.ValueOrEmpty();
}
