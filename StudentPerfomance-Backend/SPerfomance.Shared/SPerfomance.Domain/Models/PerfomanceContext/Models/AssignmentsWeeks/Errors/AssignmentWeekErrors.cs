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
        new Error($"Номера семестров должны быть следующие: {required.Number}");

    public static Error NotValidGroupContract(StudentGroup group) =>
        new Error(
            $"Невозможно начать контрольную неделю, потому что у группы {group.Name} не назначен учебный план"
        );

    public static Error TeacherAssignmentRequired(Semester semester, SemesterPlan plan) =>
        new Error(
            $"Невозможно начать контрольную неделю, потому что у семестра {semester.Number} учебного плана {semester.Plan.Direction.Name.Name} {semester.Plan.Direction.Type.Type} имеется дисциплина {plan.Discipline.Name} без назначенного преподавателя"
        );

    public static Error AssignmentAlreadyExists(
        Teacher teacher,
        SemesterPlan plan,
        Student student
    ) =>
        new Error(
            $"Проставление\nпреподаватель: {teacher.Name}\nдисциплина: {plan.Discipline}\nстудент: {student.Name}\nуже существует"
        );

    public static Error NotValidGroupActiveSemester(StudentGroup group) =>
        new Error($"У {group.Name} группы активный семестр не назначен");

    public static Error NotFound() => new Error("Контрольная неделя по указанным датам не найдена");

    public static Error InvalidStartDate() =>
        new Error("Дата начала некорректна. Дата начала не должна быть меньше текущей даты");

    public static Error InvalidEndDate() =>
        new Error("Дата закрытия некорректна. Дата закрытия должна быть от 10 дней даты начала");

    public static Error InvalidDateFormat() =>
        new Error("Некорректный формат даты. Дата принимается как День Месяц Год");

    public static Error EmptyTeacherError(
        Semester activeSemester,
        StudentGroup group,
        SemesterPlan plan
    ) =>
        new Error(
            $"У группы {group.Name} дисциплина {plan.Discipline.Name} семестра {activeSemester.Number.Number} не имеет преподавателя"
        );
}
