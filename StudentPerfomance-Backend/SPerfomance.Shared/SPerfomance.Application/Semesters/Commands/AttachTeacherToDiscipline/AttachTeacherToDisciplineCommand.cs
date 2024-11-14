using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Application.Semesters.Commands.AttachTeacherToDiscipline;

public class AttachTeacherToDisciplineCommand(Teacher? teacher, SemesterPlan? discipline)
    : ICommand<SemesterPlan>
{
    public Teacher? Teacher { get; init; } = teacher;

    public SemesterPlan? Discipline { get; init; } = discipline;
}
