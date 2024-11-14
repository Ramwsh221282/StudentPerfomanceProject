using System.Text;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Errors;

public static class ControlWeekReportErrors
{
    public static Error SessionIsNotClosed(AssignmentSession session)
    {
        var descriptionBuilder = new StringBuilder();
        descriptionBuilder
            .AppendLine(
                $"Контрольная неделя начало: {session.SessionStartDate.Date.Year}.{session.SessionStartDate.Date.Month}.{session.SessionStartDate.Date.Day}"
            )
            .AppendLine(
                $"Конец: {session.SessionStartDate.Date.Year}.{session.SessionStartDate.Date.Month}.{session.SessionStartDate.Date.Day}"
            )
            .AppendLine("Ещё не завершилась. Невозможно рассчитать статистику");
        return new Error(descriptionBuilder.ToString());
    }

    public static Error ReportDuplicateInDatabase() =>
        new Error("Отчёт за такой период уже существует");
}
