namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class CourseStatisticsReportEntity
{
    public Guid Id { get; set; }
    public Guid RootId { get; set; }
    public ControlWeekReportEntity Root { get; set; } = null!;
    public string DirectionType { get; set; } = string.Empty;
    public int Course { get; set; }
    public double Average { get; set; }
    public double Perfomance { get; set; }

    public static List<CourseStatisticsReportEntity> CreateReport(
        ControlWeekReportEntity root,
        List<GroupStatisticsReportEntity> groupStatisticsReportEntities
    )
    {
        List<CourseStatisticsReportEntity> courseStatisticsReportEntities = [];
        courseStatisticsReportEntities.AddRange(
            FillCourseStatisticsReportByBachelor(root, groupStatisticsReportEntities)
        );
        courseStatisticsReportEntities.AddRange(
            FillCourseStatisticsReportByMagister(root, groupStatisticsReportEntities)
        );
        return courseStatisticsReportEntities;
    }

    private static List<CourseStatisticsReportEntity> FillCourseStatisticsReportByBachelor(
        ControlWeekReportEntity root,
        List<GroupStatisticsReportEntity> groupStatisticsReportEntities
    )
    {
        List<CourseStatisticsReportEntity> reports = [];
        for (int i = 1; i <= 4; i++)
        {
            CourseStatisticsReportEntity report = new CourseStatisticsReportEntity();
            report.Id = Guid.NewGuid();
            report.RootId = root.Id;
            report.Root = root;
            report.DirectionType = Domain
                .Models
                .EducationDirections
                .ValueObjects
                .DirectionType
                .Bachelor
                .Type;
            report.Course = i;
            report.Average = groupStatisticsReportEntities
                .Where(g => g.Course == report.Course && g.DirectionType == report.DirectionType)
                .Select(g => g.Average)
                .Average();
            report.Perfomance = groupStatisticsReportEntities
                .Where(g => g.Course == report.Course && g.DirectionType == report.DirectionType)
                .Select(g => g.Perfomance)
                .Average();
            reports.Add(report);
        }

        return reports;
    }

    private static List<CourseStatisticsReportEntity> FillCourseStatisticsReportByMagister(
        ControlWeekReportEntity root,
        List<GroupStatisticsReportEntity> groupStatisticsReportEntities
    )
    {
        List<CourseStatisticsReportEntity> reports = [];
        for (int i = 1; i <= 2; i++)
        {
            CourseStatisticsReportEntity report = new CourseStatisticsReportEntity();
            report.Id = Guid.NewGuid();
            report.RootId = root.Id;
            report.Root = root;
            report.DirectionType = Domain
                .Models
                .EducationDirections
                .ValueObjects
                .DirectionType
                .Magister
                .Type;
            report.Course = i;
            report.Average = groupStatisticsReportEntities
                .Where(g => g.Course == report.Course && g.DirectionType == report.DirectionType)
                .Select(g => g.Average)
                .Average();
            report.Perfomance = groupStatisticsReportEntities
                .Where(g => g.Course == report.Course && g.DirectionType == report.DirectionType)
                .Select(g => g.Perfomance)
                .Average();
            reports.Add(report);
        }

        return reports;
    }
}
