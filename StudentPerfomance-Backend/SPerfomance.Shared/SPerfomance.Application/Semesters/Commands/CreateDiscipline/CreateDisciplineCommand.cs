using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.CreateDiscipline.Commands;

public class CreateDisciplineCommand(Semester? semester, string? disciplineName)
    : ICommand<SemesterPlan>
{
    public Semester? Semester { get; init; } = semester;

    public string DisciplineName { get; init; } = disciplineName.ValueOrEmpty();
}
