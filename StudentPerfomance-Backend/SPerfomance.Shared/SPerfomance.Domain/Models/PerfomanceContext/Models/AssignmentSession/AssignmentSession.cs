using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.ValueObject;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.Errors;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

public class AssignmentSession : AggregateRoot
{
    private readonly List<AssignmentWeek> _weeks = [];

    public DateTime SessionStartDate { get; private set; }

    public DateTime SessionCloseDate { get; private set; }

    public AssignmentSessionState State { get; private set; }

    private AssignmentSession()
        : base(Guid.Empty)
    {
        SessionStartDate = DateTime.MinValue;
        SessionCloseDate = DateTime.MinValue;
        State = AssignmentSessionState.Closed;
    }

    private AssignmentSession(List<StudentGroup> groups, DateTime startDate, DateTime endDate)
        : base(Guid.NewGuid())
    {
        SessionStartDate = startDate;
        SessionCloseDate = endDate;
        State = AssignmentSessionState.Opened;
        InitializeAssignmentWeeks(groups);
    }

    public IReadOnlyCollection<AssignmentWeek> Weeks => _weeks;

    internal static AssignmentSession Empty => new AssignmentSession();

    public static Result<AssignmentSession> Create(
        IReadOnlyCollection<StudentGroup> groups,
        DateTime startDate,
        DateTime endDate
    )
    {
        if (startDate.Day < DateTime.Now.Day)
            return AssignmentWeekErrors.InvalidStartDate();

        if (endDate <= startDate)
            return AssignmentWeekErrors.InvalidEndDate();

        foreach (StudentGroup group in groups)
        {
            if (group.EducationPlan == null)
                return AssignmentWeekErrors.NotValidGroupContract(group);

            if (group.ActiveGroupSemester == null)
                return AssignmentWeekErrors.NotValidGroupActiveSemester(group);

            foreach (SemesterPlan discipline in group.ActiveGroupSemester.Disciplines)
            {
                if (discipline.Teacher == null)
                    return AssignmentWeekErrors.EmptyTeacherError(
                        group.ActiveGroupSemester,
                        group,
                        discipline
                    );
            }
        }

        return new AssignmentSession(groups.ToList(), startDate, endDate);
    }

    public Result<AssignmentWeek> GetAssignmentWeek(DateTime startDate, DateTime endDate)
    {
        AssignmentWeek? week = _weeks.FirstOrDefault(w =>
            w.Session.SessionStartDate == startDate && w.Session.SessionCloseDate == endDate
        );
        if (week == null)
            return AssignmentWeekErrors.NotFound();

        return week;
    }

    private void InitializeAssignmentWeeks(List<StudentGroup> groups)
    {
        foreach (StudentGroup group in groups)
        {
            AssignmentWeek week = new AssignmentWeek(group, this);
            _weeks.Add(week);
        }
    }

    public Result<AssignmentSession> CloseSession()
    {
        if (State == AssignmentSessionState.Closed)
            return AssignmentSessionErrors.AlreadyClosed(this);

        if (
            !(
                SessionCloseDate.Day <= DateTime.Now.Day
                && SessionCloseDate.Month <= DateTime.Now.Month
                && SessionCloseDate.Year <= DateTime.Now.Year
            )
        )
            return AssignmentSessionErrors.CloseDateIsNotReached(this);

        State = AssignmentSessionState.Closed;
        return this;
    }
}
