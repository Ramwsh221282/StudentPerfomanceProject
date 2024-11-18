namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DirectionTypeStatisticsReportEntity
{
    public Guid Id { get; set; }
    public Guid RootId { get; set; }
    public ControlWeekReportEntity Root { get; set; } = null!;
    public string DirectionType { get; set; } = string.Empty;
    public double Average { get; set; }
    public double Perfomance { get; set; }

    public static List<DirectionTypeStatisticsReportEntity> CreateReport(
        ControlWeekReportEntity entity,
        List<CourseStatisticsReportEntity> courses
    )
    {
        List<DirectionTypeStatisticsReportEntity> report = [];
        DirectionTypeStatisticsReportEntity? bachelor = CreateForBachelor(entity, courses);
        DirectionTypeStatisticsReportEntity? magister = CreateMagister(entity, courses);
        if (bachelor != null)
            report.Add(bachelor);
        if (magister != null)
            report.Add(magister);
        return report;
    }

    private static DirectionTypeStatisticsReportEntity? CreateForBachelor(
        ControlWeekReportEntity entity,
        List<CourseStatisticsReportEntity> courses
    )
    {
        string type = Domain.Models.EducationDirections.ValueObjects.DirectionType.Bachelor.Type;
        CourseStatisticsReportEntity[] bachelorCourses = courses
            .Where(c => c.DirectionType == type)
            .ToArray();

        int totals = bachelorCourses.Length;
        if (totals == 0)
            return null;

        double averageSum = 0;
        double perfomanceSum = 0;
        foreach (var course in bachelorCourses)
        {
            averageSum += course.Average;
            perfomanceSum += course.Perfomance;
        }

        DirectionTypeStatisticsReportEntity report = new DirectionTypeStatisticsReportEntity();
        report.Id = Guid.NewGuid();
        report.RootId = entity.Id;
        report.Root = entity;
        report.DirectionType = type;
        report.Average = averageSum / totals;
        report.Perfomance = perfomanceSum / totals;
        return report;
    }

    private static DirectionTypeStatisticsReportEntity? CreateMagister(
        ControlWeekReportEntity entity,
        List<CourseStatisticsReportEntity> courses
    )
    {
        string type = Domain.Models.EducationDirections.ValueObjects.DirectionType.Magister.Type;
        CourseStatisticsReportEntity[] bachelorCourses = courses
            .Where(c => c.DirectionType == type)
            .ToArray();

        int totals = bachelorCourses.Length;
        if (totals == 0)
            return null;

        double averageSum = 0;
        double perfomanceSum = 0;
        foreach (var course in bachelorCourses)
        {
            averageSum += course.Average;
            perfomanceSum += course.Perfomance;
        }

        DirectionTypeStatisticsReportEntity report = new DirectionTypeStatisticsReportEntity();
        report.Id = Guid.NewGuid();
        report.RootId = entity.Id;
        report.Root = entity;
        report.DirectionType = type;
        report.Average = averageSum / totals;
        report.Perfomance = perfomanceSum / totals;
        return report;
    }
}
