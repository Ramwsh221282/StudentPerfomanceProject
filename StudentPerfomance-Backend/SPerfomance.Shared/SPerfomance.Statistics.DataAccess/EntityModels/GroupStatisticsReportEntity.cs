using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class GroupStatisticsReportEntity
{
    public Guid Id { get; set; }
    public Guid RootId { get; set; }
    public ControlWeekReportEntity Root { get; set; } = null!;
    public string DirectionCode { get; set; } = string.Empty;
    public string DirectionType { get; set; } = string.Empty;
    public byte Course { get; set; }
    public double Average { get; set; }
    public double Perfomance { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public List<DisciplinesStatisticsReportEntity> Parts { get; set; } = [];

    public static List<GroupStatisticsReportEntity> CreateReport(
        ControlWeekReportEntity entity,
        AssignmentWeekView[] views
    )
    {
        List<GroupStatisticsReportEntity> reports = new List<GroupStatisticsReportEntity>();
        foreach (var week in views)
        {
            GroupStatisticsReportEntity report = new GroupStatisticsReportEntity()
            {
                Id = week.Id,
                RootId = entity.Id,
                Root = entity,
                DirectionCode = week.Code.Code,
                DirectionType = week.Type.Type,
                Course = week.Course.Value,
                Average = week.Average,
                Perfomance = week.Perfomance,
                GroupName = week.GroupName.Name,
            };
            report.Parts = DisciplinesStatisticsReportEntity.CreateReport(report, week.Disciplines);
            reports.Add(report);
        }

        return reports;
    }
}
