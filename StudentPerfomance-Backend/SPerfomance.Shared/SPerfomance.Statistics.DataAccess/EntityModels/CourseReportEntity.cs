using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class CourseReportEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public ControlWeekReportEntity Root { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public List<CourseReportEntityPart> Parts { get; init; } = [];

    private CourseReportEntity()
    {
        Root = null!;
    }

    public CourseReportEntity(ControlWeekReportEntity root, CourseStatisticsReport report)
        : this()
    {
        Id = report.Id;
        RootId = root.Id;
        Root = root;
        Average = report.Average.ToString("F2");
        Perfomance = report.Perfomance.ToString("F0");
        foreach (var part in report.Parts)
        {
            Parts.Add(new CourseReportEntityPart(this, part));
        }
    }
}
