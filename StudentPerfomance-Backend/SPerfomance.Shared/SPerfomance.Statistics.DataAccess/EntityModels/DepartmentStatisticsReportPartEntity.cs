using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DepartmentStatisticsReportPartEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public DepartmentStatisticsReportEntity Root { get; init; }

    public string DepartmentName { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    public List<TeacherStatisticsReportPartEntity> Parts { get; init; } = [];

    private DepartmentStatisticsReportPartEntity()
    {
        Root = null!;
        DepartmentName = string.Empty;
    }

    public DepartmentStatisticsReportPartEntity(
        DepartmentStatisticsReportEntity root,
        DepartmentStatisticsReportPart part
    )
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        DepartmentName = part.DepartmentName.Name;
        Average = part.Average.ToString("F2");
        Perfomance = part.Perfomance.ToString("F0");
        foreach (var teacherPart in part.Parts)
        {
            Parts.Add(new TeacherStatisticsReportPartEntity(this, teacherPart));
        }
    }
}
