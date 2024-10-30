using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.SemesterPlans.Errors;

public static class SemesterPlanErrors
{
	public static Error TeacherAlreadyAttacher() => new Error("Для этой дисциплины уже задан преподаватель");

	public static Error TeacherIsNotAttached() => new Error("Преподаватель уже не назначен");

	public static Error DisciplineNameEmpty() => new Error("Имя дисциплины не было указано");

	public static Error DisciplineLengthExceess(int length) =>
		new Error($"Название дисциплины превышает {length} символов");

	public static Error DisciplineLengthLess(int length) =>
		new Error($"Названиед дисциплины менее {length} символов");

	public static Error DisciplineDublicate() => new Error("Дисциплина уже существует в этом семестре");

	public static Error NotFound() => new Error("Дисциплина не найдена");

	public static Error TeacherHasDisciplineAlready(Teacher teacher, SemesterPlan plan) =>
		new Error($"Преподаватель {teacher.Name.Name} {teacher.Name.Surname} {teacher.Name.Patronymic} уже ведет дисциплину {plan.Discipline.Name}");

	public static Error TeacherDoesntChargeDiscipline(Teacher teacher) =>
		new Error($"Преподаватель {teacher.Name.Name} {teacher.Name.Surname} {teacher.Name.Patronymic} не ведет эту дисциплину");

	public static Error DoesntBelongSemester() => new Error("Дисциплина не принадлежит этому семестру");
}
