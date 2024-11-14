using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class GroupStatisticsReportEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public ControlWeekReportEntity Root { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public string GroupName { get; init; }

    public byte AtSemester { get; init; }

    public string DirectionCode { get; init; }

    public string DirectionType { get; init; }

    public List<StudentStatisticsPartEntity> Parts { get; init; } = [];

    private GroupStatisticsReportEntity()
    {
        Root = null!;
        GroupName = string.Empty;
        DirectionCode = string.Empty;
        DirectionType = string.Empty;
    }

    public GroupStatisticsReportEntity(ControlWeekReportEntity root, GroupStatisticsReport report)
        : this()
    {
        Id = report.Id;
        RootId = report.Root.Id;
        Root = root;
        Average = report.Average.ToString("F2");
        Perfomance = report.Perfomance.ToString("F0");
        GroupName = report.StudentGroupName.Name;
        AtSemester = report.AtSemester.Number;
        DirectionCode = report.DirectionCode.Code;
        DirectionType = report.DirectionType.Type;
        foreach (var part in report.Parts)
        {
            Parts.Add(new StudentStatisticsPartEntity(this, part));
        }
    }
}
