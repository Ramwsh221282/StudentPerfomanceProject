using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class CourseReportEntityPart
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public CourseReportEntity Root { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public byte Course { get; init; }

    private CourseReportEntityPart()
    {
        Root = null!;
    }

    public CourseReportEntityPart(CourseReportEntity root, CoursePart part)
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        Average = part.Average.ToString("F2");
        Perfomance = part.Perfomance.ToString("F0");
        Course = part.Course.Value;
    }
}
