namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DirectionCodeStatisticsReportEntity
{
    public Guid Id { get; set; }
    public Guid RootId { get; set; }
    public ControlWeekReportEntity Root { get; set; } = null!;
    public string DirectionCode { get; set; } = string.Empty;
    public double Average { get; set; }
    public double Perfomance { get; set; }

    public static List<DirectionCodeStatisticsReportEntity> CreateReport(
        ControlWeekReportEntity root,
        List<GroupStatisticsReportEntity> groupStatisticsReportEntities
    )
    {
        List<DirectionCodeStatisticsReportEntity> reports = [];
        HashSet<string> codes = groupStatisticsReportEntities
            .Select(g => g.DirectionCode)
            .ToHashSet();
        foreach (var code in codes)
        {
            DirectionCodeStatisticsReportEntity report = new DirectionCodeStatisticsReportEntity();
            report.Id = Guid.NewGuid();
            report.RootId = root.Id;
            report.DirectionCode = code;

            GroupStatisticsReportEntity[] groups = groupStatisticsReportEntities
                .Where(g => g.DirectionCode == code)
                .ToArray();

            if (groups.Length == 0)
                continue;

            double average = 0;
            double perfomance = 0;
            foreach (var group in groups)
            {
                average += group.Average;
                perfomance += group.Perfomance;
            }

            report.Average = average / groups.Length;
            report.Perfomance = perfomance / groups.Length;
            reports.Add(report);
        }

        return reports;
    }
}
