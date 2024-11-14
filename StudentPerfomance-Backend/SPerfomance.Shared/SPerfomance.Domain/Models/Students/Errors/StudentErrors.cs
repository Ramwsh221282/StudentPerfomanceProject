using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Students.Errors;

public static class StudentErrors
{
    public static Error NameEmptyError() => new Error("Имя студента было пустым");

    public static Error SurnameEmptyError() => new Error("Фамилия студента была пустой");

    public static Error NameExceess(int length) =>
        new Error($"Имя студента более {length} символов");

    public static Error SurnameExceess(int length) =>
        new Error($"Имя студента менее {length} символов");

    public static Error PatronymicExceess(int length) =>
        new Error($"Отчество студента менее {length} символов");

    public static Error InvalidRecordbook(ulong recordbook) =>
        new Error($"Зачётная книжка {recordbook} недопустима");

    public static Error InvalidState(string state) =>
        new Error($"Состояние студента {state} недопустимо");

    public static Error StateEmpty() => new Error("Состояние студента не было указано");

    public static Error StudentDoesntBelongGroup(StudentGroup group) =>
        new Error($"Студент не принадлежит {group.Name.Name}");

    public static Error StudentDublicateError(StudentGroup group) =>
        new Error($"Студент уже состоит в {group.Name.Name}");

    public static Error NotFound() => new Error("Студент не найден");

    public static Error RecordbookDublicate(ulong recordBook) =>
        new Error($"Зачетная книжка {recordBook} уже занята");
}
