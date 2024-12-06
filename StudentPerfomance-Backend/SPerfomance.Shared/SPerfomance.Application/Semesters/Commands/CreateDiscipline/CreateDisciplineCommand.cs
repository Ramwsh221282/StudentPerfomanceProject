using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Semesters.Commands.CreateDiscipline;

/// <summary>
/// Команда добавления дисциплины в семестр.
/// </summary>
/// <param name="semester">Семестр, в который необходимо добавить дисциплину</param>
/// <param name="disciplineName">Наименование дисциплины</param>
public class CreateDisciplineCommand(Semester? semester, string? disciplineName)
    : ICommand<SemesterPlan>
{
    public Semester? Semester { get; init; } = semester;

    public string DisciplineName { get; init; } = disciplineName.ValueOrEmpty();
}
