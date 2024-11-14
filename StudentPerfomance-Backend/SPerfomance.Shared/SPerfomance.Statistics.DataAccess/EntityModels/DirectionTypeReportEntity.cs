using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionTypeReports;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DirectionTypeReportEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public ControlWeekReportEntity Root { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public List<DirectionTypeReportEntityPart> Parts { get; init; } = [];

    private DirectionTypeReportEntity()
    {
        Root = null!;
    }

    public DirectionTypeReportEntity(ControlWeekReportEntity root, DirectionTypeReport report)
        : this()
    {
        Id = report.Id;
        RootId = root.Id;
        Root = root;
        Average = report.Average.ToString("F2");
        Perfomance = report.Perfomance.ToString("F0");
        foreach (var part in report.Parts)
        {
            Parts.Add(new DirectionTypeReportEntityPart(this, part));
        }
    }
}
