using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports.Parts;

public static class GroupStatisticsReportErrors
{
    public static Error StudentStatisticsDublicateError(StudentStatisticsPart part)
    {
        return new Error($"Информация о {part.Name} {part.Recordbook} уже есть в этом отчёте");
    }
}