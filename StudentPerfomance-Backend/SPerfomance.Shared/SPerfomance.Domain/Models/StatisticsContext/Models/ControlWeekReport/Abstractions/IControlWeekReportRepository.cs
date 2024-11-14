using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Abstractions;

public interface IControlWeekReportRepository
{
    public Task<Result<ControlWeekReport>> Insert(ControlWeekReport report);
}
