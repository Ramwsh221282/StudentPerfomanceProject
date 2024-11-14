using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class ControlWeekReportEntity
{
    public int RowNumber { get; private set; }

    public Guid Id { get; init; }

    public DateTime CreationDate { get; init; }

    public DateTime CompletionDate { get; init; }

    public bool IsFinished { get; init; }

    public List<GroupStatisticsReportEntity> GroupParts { get; init; } = [];

    public CourseReportEntity CourseReport { get; init; }

    public DirectionTypeReportEntity DirectionTypeReport { get; init; }

    public DirectionCodeReportEntity DirectionCodeReport { get; init; }

    public DepartmentStatisticsReportEntity DepartmentReport { get; init; }

    private ControlWeekReportEntity()
    {
        CourseReport = null!;
        DirectionTypeReport = null!;
        DirectionCodeReport = null!;
        DepartmentReport = null!;
    }

    public ControlWeekReportEntity(ControlWeekReport report)
        : this()
    {
        Id = report.Id;
        CreationDate = report.Period.CreationDate;
        CompletionDate = report.Period.CompletionDate;
        IsFinished = report.IsFinished;
        foreach (var groupPart in report.GroupReports)
        {
            GroupParts.Add(new GroupStatisticsReportEntity(this, groupPart));
        }
        CourseReport = new CourseReportEntity(this, report.CourseReport);
        DirectionTypeReport = new DirectionTypeReportEntity(this, report.DirectionTypeReport);
        DirectionCodeReport = new DirectionCodeReportEntity(this, report.DirectionCodeReport);
        DepartmentReport = new DepartmentStatisticsReportEntity(
            this,
            report.DepartmentStatisticsReport
        );
    }

    public void SetEntityNumber(int number) => RowNumber = number;
}
