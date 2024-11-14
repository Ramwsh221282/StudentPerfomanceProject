using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;

public static class AssignmentSessionErrors
{
    public static Error NoActiveFound() =>
        new Error("В данный момент нет ни одной активной контрольной недели");

    public static Error CloseDateIsNotReached(AssignmentSession session) =>
        new Error(
            $"Нельзя закрыть контрольную неделю, поскольку дата окончания - {session.SessionCloseDate.ToString("dd/MM/yyyy")} не достигнута"
        );

    public static Error CantFindById(Guid id) =>
        new Error($"Контрольная неделя с ID {id} не найдена");

    public static Error AlreadyClosed(AssignmentSession session) =>
        new Error(
            $"Контрольная неделя с периодом: {session.SessionCloseDate.ToString("dd/MM/yyyy")} - {session.SessionCloseDate.ToString("dd/MM/yyyy")} уже закрыта"
        );
}
