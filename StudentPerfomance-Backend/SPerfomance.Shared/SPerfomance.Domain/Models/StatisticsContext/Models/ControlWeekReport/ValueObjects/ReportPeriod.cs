using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ValueObjects;

public sealed class ReportPeriod : DomainValueObject
{
    public DateTime CreationDate { get; init; }

    public DateTime CompletionDate { get; init; }

    internal ReportPeriod()
    {
        CreationDate = DateTime.MinValue;
        CompletionDate = DateTime.MinValue;
    }

    internal ReportPeriod(AssignmentSession session)
    {
        CreationDate = DateTime.Now;
        CompletionDate = session.SessionCloseDate.Date;
    }

    internal ReportPeriod(DateTime startDate, DateTime closeDate)
    {
        CreationDate = startDate;
        CompletionDate = closeDate;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return CreationDate;
        yield return CompletionDate;
    }
}
