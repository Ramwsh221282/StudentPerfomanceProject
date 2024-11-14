using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;

namespace SPerfomance.Application.Semesters.Commands.DeattachTeacherFromDiscipline;

public class DeattachTeacherFromDisciplineCommand(SemesterPlan? discipline) : ICommand<SemesterPlan>
{
    public SemesterPlan? Discipline { get; init; } = discipline;
}
