using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Teachers.Errors;

public static class TeacherErrors
{
    public static Error NameEmpty() => new Error("Имя преподавателя было пустым");

    public static Error SurnameEmpty() => new Error("Фамилия преподавателя была пустая");

    public static Error NameExceess(int length) =>
        new Error($"Имя преподавателя более {length} символов");

    public static Error SurnameExceess(int length) =>
        new Error($"Фамилия преподавателя более {length} символов");

    public static Error PatronymicExceess(int length) =>
        new Error($"Отчество преподавателя более {length} символов");

    public static Error JobTitleEmpty() => new Error("Должность преподавателя была пустой");

    public static Error JobTitleInvalid(string jobTitle) =>
        new Error($"Должность {jobTitle} недопустима");

    public static Error WorkingStateEmpty() =>
        new Error("Условие работы преподавателя было пустым");

    public static Error WorkingStateInvalid(string state) =>
        new Error($"Условие работы {state} недопустимо");

    public static Error NotFound() => new Error("Преподаватель не был найден");

    public static Error WithoutDepartment() => new Error("Кафедра не была указана");

    public static Error DoesntBelongDepartment(TeachersDepartments department) =>
        new Error($"Данный преподаватель не принадлежит кафедре {department.Name.Name}");

    public static Error TeacherDuplicate() =>
        new("Преподаватель с такими данными уже есть в системе");
}
