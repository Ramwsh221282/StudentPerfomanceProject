using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentStats;

public sealed class AssignmentStat : DomainEntity
{
	public AssignmentWeek Week { get; init; }

	public double AverageGroupPerfomance { get; init; }

	public double AverageGroupPerfomancePercent { get; init; }

	internal AssignmentStat(Guid id, AssignmentWeek week) : base(id)
	{
		Week = week;
		AverageGroupPerfomance = 0;
		AverageGroupPerfomancePercent = 0;
	}

	internal Result<AssignmentStat> CalculateAverageGroupWeekPerfomance()
	{
		throw new NotImplementedException();
	}

	internal Result<AssignmentStat> CalculateAverageGroupWeekPerfomancePercent()
	{
		throw new NotImplementedException();
	}
}
