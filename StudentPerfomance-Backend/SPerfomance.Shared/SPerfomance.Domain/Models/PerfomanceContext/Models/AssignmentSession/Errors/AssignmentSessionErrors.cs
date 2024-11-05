using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;

public static class AssignmentSessionErrors
{
	public static Error NoActiveFound() => new Error("В данный момент нет ни одной активной контрольной недели");
}
