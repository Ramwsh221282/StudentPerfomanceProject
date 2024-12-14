using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Semesters.ValueObjects;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.Errors;

public static class AssignmentWeekErrors
{
    public static Error NotValidSemesters(SemesterNumber required) =>
        new($"Номера семестров должны быть следующие: {required.Number}");

    public static Error NotValidGroupContract(StudentGroup group) =>
        new(
            $"Невозможно начать контрольную неделю, потому что у группы {group.Name} не назначен учебный план"
        );

    public static Error TeacherAssignmentRequired(Semester semester, SemesterPlan plan) =>
        new(
            $"Невозможно начать контрольную неделю, потому что у семестра {semester.Number} учебного плана {semester.Plan.Direction.Name.Name} {semester.Plan.Direction.Type.Type} имеется дисциплина {plan.Discipline.Name} без назначенного преподавателя"
        );

    public static Error AssignmentAlreadyExists(
        Teacher teacher,
        SemesterPlan plan,
        Student student
    ) =>
        new(
            $"Проставление\nпреподаватель: {teacher.Name}\nдисциплина: {plan.Discipline}\nстудент: {student.Name}\nуже существует"
        );

    public static Error NotValidGroupActiveSemester(StudentGroup group) =>
        new($"У {group.Name} группы активный семестр не назначен");

    public static Error NotFound() => new("Контрольная неделя по указанным датам не найдена");

    public static Error InvalidStartDate() =>
        new("Дата начала некорректна. Дата начала не должна быть меньше текущей даты");

    public static Error InvalidEndDate() =>
        new("Дата закрытия некорректна. Дата закрытия должна быть от 10 дней даты начала");

    public static Error InvalidDateFormat() =>
        new("Некорректный формат даты. Дата принимается как День Месяц Год");

    public static Error EmptyTeacherError(
        Semester activeSemester,
        StudentGroup group,
        SemesterPlan plan
    ) =>
        new(
            $"У группы {group.Name} дисциплина {plan.Discipline.Name} семестра {activeSemester.Number.Number} не имеет преподавателя"
        );

    public static Error ActiveSemesterHasNoDisciplines(StudentGroup group) =>
        new(
            $"У текущего ({group.ActiveGroupSemester!.Number.Number}) семестра группы {group.Name.Name} нет дисциплин"
        );

    public static Error GroupHasNoStudents(StudentGroup group) =>
        new($"{group.Name.Name} не содержит студентов.");

    public static Error InvalidPeriodByYear(ref DateTime startYear, ref DateTime endYear) =>
        new(
            $"Период контрольной недели некорректный, поскольку года слишком отличаются ({startYear.Year} - {endYear.Year})"
        );
}
