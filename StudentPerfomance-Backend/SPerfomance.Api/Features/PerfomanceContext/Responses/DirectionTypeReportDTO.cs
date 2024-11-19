using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed record DirectionTypeReportDTO
{
    public Guid Id { get; set; }
    public string DirectionType { get; set; } = string.Empty;
    public double Average { get; set; }
    public double Perfomance { get; set; }

    public DirectionTypeReportDTO(DirectionTypeStatisticsReportEntity report)
    {
        Id = report.Id;
        DirectionType = report.DirectionType;
        Average = Math.Round(report.Average, 2);
        Perfomance = Math.Round(report.Perfomance, 2);
    }
}
