namespace SPerfomance.Api.Features.PerfomanceContext.Contracts;

public record ReportPeriodFilterContract(DatePeriod? StartPeriod, DatePeriod? EndPeriod);

public record DatePeriod(int? Day, int? Month, int? Year);
