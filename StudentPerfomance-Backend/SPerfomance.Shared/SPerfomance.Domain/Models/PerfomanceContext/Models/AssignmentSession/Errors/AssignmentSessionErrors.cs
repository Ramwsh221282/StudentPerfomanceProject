using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;

public static class AssignmentSessionErrors
{
    public static Error NoActiveFound() =>
        new("В данный момент нет ни одной активной контрольной недели");

    public static Error CloseDateIsNotReached(AssignmentSession session) =>
        new(
            $"Нельзя закрыть контрольную неделю, поскольку дата окончания - {session.SessionCloseDate.ToString("dd/MM/yyyy")} не достигнута"
        );

    public static Error CantFindById(Guid id) => new($"Контрольная неделя с ID {id} не найдена");

    public static Error AlreadyClosed(AssignmentSession session) =>
        new(
            $"Контрольная неделя с периодом: {session.SessionCloseDate.ToString("dd/MM/yyyy")} - {session.SessionCloseDate.ToString("dd/MM/yyyy")} уже закрыта"
        );

    public static Error AssignmentSessionNumberNotAllowed(byte number) =>
        new($"Номер {number} для создания контрольной недели не допустим");

    public static Error AssignmentSessionSemesterTypeEmpty() =>
        new("Сезон семестра при создании контрольной недели не был указан");

    public static Error AssignmentSessionSemesterTypeInvalid(string type) =>
        new($"Сезон семестра {type} не допускается при создании контрольной недели");

    public static Error AssignmentSessionSemesterNumberEmpty() =>
        new("Номер контрольной недели не был указан");

    public static Error AssignmentSessionPeriodInvalid(
        ref DateTime startDate,
        ref DateTime endDate,
        ref int badDifference
    )
    {
        if (badDifference == 0)
        {
            return new Error(
                $"Период контрольной недели: {startDate.ToString("dd/MM/yyyy")} - {endDate.ToString("dd/MM/yyyy")} не является корректным периодом контрольной недели"
            );
        }

        return new Error(
            $"Период контрольной недели: {startDate.ToString("dd/MM/yyyy")} - {endDate.ToString("dd/MM/yyyy")} не является корректным периодом контрольной недели, поскольку разность дней не равна 7 (рассчитанная разность - {badDifference})"
        );
    }

    public static Error AssignmentSessionAutumnPeriodIncorrect(ref DateTime startDate) =>
        new($"Месяц {startDate.Month} не принадлежит осеннему срезу");

    public static Error AssignmentSessionSpringPeriodIncorrect(ref DateTime startDate) =>
        new($"Месяц {startDate.Month} не принадлежит весеннему срезу");
}
