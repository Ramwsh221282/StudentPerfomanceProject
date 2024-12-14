using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionCodeReports;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionTypeReports;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Errors;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport;

public sealed class ControlWeekReport : AggregateRoot
{
    private readonly List<GroupStatisticsReport> _groupReports = [];

    public bool IsFinished { get; init; }

    public ReportPeriod Period { get; init; }

    public IReadOnlyCollection<GroupStatisticsReport> GroupReports => _groupReports;

    public CourseStatisticsReport CourseReport { get; init; }

    public DirectionTypeReport DirectionTypeReport { get; init; }

    public DirectionCodeReport DirectionCodeReport { get; init; }

    public DepartmentStatisticsReport DepartmentStatisticsReport { get; init; }

    private ControlWeekReport(Guid id)
        : base(id)
    {
        IsFinished = false;
        Period = null!;
        CourseReport = null!;
        DirectionCodeReport = null!;
        DirectionTypeReport = null!;
        DepartmentStatisticsReport = null!;
    }

    private ControlWeekReport(AssignmentSession session)
        : this(session.Id)
    {
        IsFinished = session.State.State;
        Period = new ReportPeriod(session);
        var fillGroupTask = FillGroupReports(session);
        var createCourseReportTask = CreateCourseStatistics(session);
        var createDirectionTypeReportTask = CreateDirectionTypeReport(session);
        var createDirectionCodeReportTask = CreateDirectionCodeReport(session);
        var createDepartmentReportTask = CreateDepartmentReport(session);
        Task.WhenAll(
                fillGroupTask,
                createCourseReportTask,
                createCourseReportTask,
                createDirectionCodeReportTask,
                createDepartmentReportTask
            )
            .Wait();
        CourseReport = createCourseReportTask.Result;
        DirectionTypeReport = createDirectionTypeReportTask.Result;
        DirectionCodeReport = createDirectionCodeReportTask.Result;
        DepartmentStatisticsReport = createDepartmentReportTask.Result;
    }

    public static Result<ControlWeekReport> Create(AssignmentSession session)
    {
        if (session.State == AssignmentSessionState.Opened)
            return ControlWeekReportErrors.SessionIsNotClosed(session);
        return new ControlWeekReport(session);
    }

    private async Task FillGroupReports(AssignmentSession session)
    {
        foreach (var week in session.Weeks)
        {
            GroupStatisticsReport report = new GroupStatisticsReport(Guid.NewGuid(), this, week);
            _groupReports.Add(report);
        }
        await Task.CompletedTask;
    }

    private async Task<CourseStatisticsReport> CreateCourseStatistics(AssignmentSession session)
    {
        CourseStatisticsReport report = new CourseStatisticsReport(Guid.NewGuid(), this, session);
        return await Task.FromResult(report);
    }

    private async Task<DirectionTypeReport> CreateDirectionTypeReport(AssignmentSession session)
    {
        DirectionTypeReport report = new DirectionTypeReport(Guid.NewGuid(), this, session);
        return await Task.FromResult(report);
    }

    private async Task<DirectionCodeReport> CreateDirectionCodeReport(AssignmentSession session)
    {
        DirectionCodeReport report = new DirectionCodeReport(Guid.NewGuid(), this, session);
        return await Task.FromResult(report);
    }

    private async Task<DepartmentStatisticsReport> CreateDepartmentReport(AssignmentSession session)
    {
        DepartmentStatisticsReport report = new DepartmentStatisticsReport(
            Guid.NewGuid(),
            this,
            session
        );
        return await Task.FromResult(report);
    }
}
