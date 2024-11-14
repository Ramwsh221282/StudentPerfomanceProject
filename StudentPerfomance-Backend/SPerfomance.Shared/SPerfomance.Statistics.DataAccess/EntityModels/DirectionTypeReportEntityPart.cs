using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionTypeReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DirectionTypeReportEntityPart
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public DirectionTypeReportEntity Root { get; init; }

    public string DirectionType { get; init; }

    public string Perfomance { get; init; }

    public string Average { get; init; }

    private DirectionTypeReportEntityPart()
    {
        Root = null!;
        DirectionType = string.Empty;
    }

    public DirectionTypeReportEntityPart(
        DirectionTypeReportEntity root,
        DirectionTypeReportPart part
    )
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        DirectionType = part.Type.Type;
        Perfomance = part.Perfomance.ToString("F2");
        Average = part.Average.ToString("F0");
    }
}
