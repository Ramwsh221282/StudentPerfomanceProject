using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.TeacherDepartments.Errors;

public static class TeacherDepartmentErrors
{
    public static Error DepartmentNameEmptyError() => new Error("Название кафедры было пустым");

    public static Error DepartmentNameExceessError(int length) =>
        new Error($"Название кафедры более {length} символов");

    public static Error DepartmentNameLessError(int length) =>
        new Error($"Название кафедры менее {length} символов");

    public static Error DepartmentDublicate(string name) =>
        new Error($"Кафедра {name} уже существует");

    public static Error NotFound() => new Error("Кафедра не найдена");
}
