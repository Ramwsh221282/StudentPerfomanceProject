using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StudentGroups.Errors;

public static class StudentGroupErrors
{
    public static Error EducationPlanHasGroupAlreadyError(EducationPlan plan, StudentGroup group) =>
        new Error(
            $"Учебный план {plan.Year} {plan.Direction.Code.Code} {plan.Direction.Name.Name} уже содержит группу {group.Name.Name}"
        );

    public static Error StudentGroupDoesntBelongEducationPlan(
        EducationPlan plan,
        StudentGroup group
    ) =>
        new Error(
            $"Группа {group.Name.Name} не принадлежит учебному плану {plan.Year} {plan.Direction.Code.Code} {plan.Direction.Name.Name}"
        );

    public static Error NameEmpty() => new Error("Название группы не указано");

    public static Error NameExceess(int length) =>
        new Error($"Название группы превышает {length} символов");

    public static Error NameLess(int length) =>
        new Error($"Название группы менее {length} символов");

    public static Error NameInvalid(string name) =>
        new Error($"Название группы {name} недопустимо");

    public static Error NameDublicate(string name) => new Error($"Название группы {name} занято");

    public static Error NotFound() => new Error("Группа не найдена");

    public static Error CantMergeWithSame() => new Error("Нельзя смешать группу саму с собой");
}
