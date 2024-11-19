using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed record CourseStatisticsReportDTO
{
    public Guid Id { get; init; }
    public string DirectionType { get; init; }
    public int Course { get; init; }
    public double Average { get; init; }
    public double Perfomance { get; init; }

    public CourseStatisticsReportDTO(CourseStatisticsReportEntity report)
    {
        Id = report.Id;
        DirectionType = report.DirectionType;
        Course = report.Course;
        Average = Math.Round(report.Average, 2);
        Perfomance = Math.Round(report.Perfomance, 2);
    }
}
