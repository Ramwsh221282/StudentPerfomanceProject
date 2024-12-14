using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.Errors;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;

public class AssignmentSession : AggregateRoot
{
    private readonly List<AssignmentWeek> _weeks = [];

    public DateTime SessionStartDate { get; }

    public DateTime SessionCloseDate { get; }

    public AssignmentSessionState State { get; private set; }

    public AssignmentSessionNumber Number { get; private set; }

    public AssignmentSessionSemesterType Type { get; private set; }

    private AssignmentSession()
        : base(Guid.Empty)
    {
        Number = default!;
        Type = default!;
        SessionStartDate = DateTime.MinValue;
        SessionCloseDate = DateTime.MinValue;
        State = AssignmentSessionState.Closed;
    }

    private AssignmentSession(
        List<StudentGroup> groups,
        DateTime startDate,
        AssignmentSessionNumber number,
        AssignmentSessionSemesterType type
    )
        : base(Guid.NewGuid())
    {
        SessionStartDate = startDate;
        SessionCloseDate = startDate.AddDays(7);
        State = AssignmentSessionState.Opened;
        Number = number;
        Type = type;
        InitializeAssignmentWeeks(groups);
    }

    public IReadOnlyCollection<AssignmentWeek> Weeks => _weeks;

    internal static AssignmentSession Empty => new AssignmentSession();

    public static Result<AssignmentSession> Create(
        IReadOnlyCollection<StudentGroup> groups,
        DateTime startDate,
        AssignmentSessionNumber number,
        AssignmentSessionSemesterType type
    )
    {
        if (!IsValidByAutumn(ref startDate, type.Type))
            return AssignmentSessionErrors.AssignmentSessionAutumnPeriodIncorrect(ref startDate);

        if (!IsValidBySpring(ref startDate, type.Type))
            return AssignmentSessionErrors.AssignmentSessionSpringPeriodIncorrect(ref startDate);

        foreach (StudentGroup group in groups)
        {
            if (IsGroupHasNoStudents(group))
                return AssignmentWeekErrors.GroupHasNoStudents(group);

            if (IsGroupEducationPlanEmpty(group))
                return AssignmentWeekErrors.NotValidGroupContract(group);

            if (IsGroupActiveSemesterEmpty(group))
                return AssignmentWeekErrors.NotValidGroupActiveSemester(group);

            if (IsGroupActiveSemesterHasNoDisciplines(group))
                return AssignmentWeekErrors.ActiveSemesterHasNoDisciplines(group);

            var groupDisciplines = group.ActiveGroupSemester!.Disciplines;

            foreach (var discipline in groupDisciplines)
            {
                if (IsDisciplineWithoutTeacher(discipline))
                    return AssignmentWeekErrors.EmptyTeacherError(
                        group.ActiveGroupSemester,
                        group,
                        discipline
                    );
            }
        }

        return new AssignmentSession(groups.ToList(), startDate, number, type);
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

    public Result<AssignmentSession> CloseSession()
    {
        //
        // if (
        //     !(
        //         SessionCloseDate.Day <= DateTime.Now.Day
        //         && SessionCloseDate.Month <= DateTime.Now.Month
        //         && SessionCloseDate.Year <= DateTime.Now.Year
        //     )
        // )
        //     return AssignmentSessionErrors.CloseDateIsNotReached(this);

        State = AssignmentSessionState.Closed;
        return this;
    }

    private void InitializeAssignmentWeeks(List<StudentGroup> groups)
    {
        foreach (StudentGroup group in groups)
        {
            AssignmentWeek week = new AssignmentWeek(group, this);
            _weeks.Add(week);
        }
    }

    private static bool IsValidByAutumn(ref DateTime startDate, string season)
    {
        if (season != "Осень")
            return true;
        var month = startDate.Month;
        return month >= 9 && month <= 12;
    }

    private static bool IsValidBySpring(ref DateTime startDate, string season)
    {
        if (season != "Весна")
            return true;
        var month = startDate.Month;
        return month >= 1 && month <= 6;
    }

    private static bool IsGroupHasNoStudents(StudentGroup group) => group.Students.Count == 0;

    private static bool IsGroupEducationPlanEmpty(StudentGroup group) =>
        group.EducationPlan == null;

    private static bool IsGroupActiveSemesterEmpty(StudentGroup group) =>
        group.ActiveGroupSemester == null;

    private static bool IsGroupActiveSemesterHasNoDisciplines(StudentGroup group) =>
        group.ActiveGroupSemester!.Disciplines.Count == 0;

    private static bool IsDisciplineWithoutTeacher(SemesterPlan plan) => plan.Teacher == null;
}
