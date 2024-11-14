using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionCodeReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DirectionCodeReportPartEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public DirectionCodeReportEntity Root { get; init; }

    public string Code { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    private DirectionCodeReportPartEntity()
    {
        Root = null!;
        Code = string.Empty;
    }

    public DirectionCodeReportPartEntity(
        DirectionCodeReportEntity root,
        DirectionCodeReportPart part
    )
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        Average = part.Average.ToString("F2");
        Perfomance = part.Perfomance.ToString("F0");
        Code = part.Code.Code;
    }
}
