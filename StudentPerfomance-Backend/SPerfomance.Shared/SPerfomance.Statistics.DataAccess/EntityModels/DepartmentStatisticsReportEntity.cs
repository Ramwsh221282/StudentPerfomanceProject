using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DepartmentStatisticsReportEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public ControlWeekReportEntity Root { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public List<DepartmentStatisticsReportPartEntity> Parts { get; init; } = [];

    private DepartmentStatisticsReportEntity()
    {
        Root = null!;
    }

    public DepartmentStatisticsReportEntity(
        ControlWeekReportEntity root,
        DepartmentStatisticsReport report
    )
        : this()
    {
        Id = report.Id;
        RootId = root.Id;
        Root = root;
        Average = report.Average.ToString("F2");
        Perfomance = report.Perfomance.ToString("F0");
        foreach (var part in report.Parts)
        {
            Parts.Add(new DepartmentStatisticsReportPartEntity(this, part));
        }
    }
}
