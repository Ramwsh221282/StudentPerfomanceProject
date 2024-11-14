namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Abstractions;

public interface IStatisticsReportPart
{
    public double Average { get;  }
    
    public double Perfomance { get; }
}