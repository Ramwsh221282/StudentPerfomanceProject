using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Errors;

public static class AssignmentErrors
{
    public static Error InvalidAssignmentValue(int value) =>
        new Error($"Оценка {value} недопустима в контексте");

    public static Error NotFound() => new Error("Проставление не найдено");

    public static Error GroupAssignmentsCompleted(StudentGroup group) =>
        new Error($"По группе {group.Name} уже есть все проставления в этой контрольной неделе");

    public static Error NotExist(Student student, SemesterPlan discipline) =>
        new Error(
            $"Пара студент/дисциплина {student.Name} ${discipline.Discipline.Name} не найдены в этом списке контрольной недели"
        );
}
