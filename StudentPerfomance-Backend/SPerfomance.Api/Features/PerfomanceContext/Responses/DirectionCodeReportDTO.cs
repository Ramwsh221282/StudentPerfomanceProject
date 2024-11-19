using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed record DirectionCodeReportDTO
{
    public Guid Id { get; init; }
    public string DirectionCode { get; init; }
    public double Average { get; init; }
    public double Perfomance { get; init; }

    public DirectionCodeReportDTO(DirectionCodeStatisticsReportEntity report)
    {
        Id = report.Id;
        DirectionCode = report.DirectionCode;
        Average = Math.Round(report.Average, 2);
        Perfomance = Math.Round(report.Perfomance, 2);
    }
}
