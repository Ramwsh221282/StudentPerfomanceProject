using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationPlans.Errors;

public static class EducationPlanErrors
{
    public static Error YearExceessError(int length) =>
        new Error($"Год направления подготовки превышает {length}");

    public static Error YearLessError(int length) =>
        new Error($"Год направления подготовки менее {length}");

    public static Error DublicateError(EducationDirection direction, int year) =>
        new Error(
            $"Учебный план {direction.Code.Code} {direction.Name.Name} {year} уже существует"
        );

    public static Error NotFoundError() => new Error("Учебный план не найден");

    public static Error WithoutDirection() =>
        new Error("Учебный план нельзя создать без указания направления подготовки");

    public static Error YearEmpty() => new Error("Год учебного плана не может быть пустым");

    public static Error NullError() => new Error("Учебный план не указан");
}
