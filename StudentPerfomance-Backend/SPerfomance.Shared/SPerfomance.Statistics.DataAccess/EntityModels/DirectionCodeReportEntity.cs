using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionCodeReports;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DirectionCodeReportEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public ControlWeekReportEntity Root { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public List<DirectionCodeReportPartEntity> Parts { get; init; } = [];

    private DirectionCodeReportEntity()
    {
        Root = null!;
    }

    public DirectionCodeReportEntity(ControlWeekReportEntity root, DirectionCodeReport report)
        : this()
    {
        Id = report.Id;
        RootId = root.Id;
        Root = root;
        Average = report.Average.ToString("F2");
        Perfomance = report.Perfomance.ToString("F0");
        foreach (var part in report.Parts)
        {
            Parts.Add(new DirectionCodeReportPartEntity(this, part));
        }
    }
}
