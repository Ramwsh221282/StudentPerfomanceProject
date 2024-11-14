using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentStats;

public class StudentAssignmentStats : DomainEntity
{
    internal AssignmentWeek Week { get; init; }

    internal StudentAssignmentStats(Guid id, AssignmentWeek week)
        : base(id)
    {
        Week = week;
    }
}
