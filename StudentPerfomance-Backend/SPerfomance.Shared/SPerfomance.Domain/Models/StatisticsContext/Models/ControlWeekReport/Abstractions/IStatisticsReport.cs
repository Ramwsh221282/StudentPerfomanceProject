namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Abstractions;

public interface  IStatisticsReport
{
    public IReadOnlyCollection<IStatisticsReportPart> Parts { get; }
    
    public double Perfomance { get; }

    public double Average { get; }
}