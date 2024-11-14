using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class StudentStatisticsOnDisciplinePartEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public StudentStatisticsPartEntity Root { get; init; }

    public string DisciplineName { get; init; }

    private StudentStatisticsOnDisciplinePartEntity()
    {
        Root = null!;
        DisciplineName = string.Empty;
    }

    public StudentStatisticsOnDisciplinePartEntity(
        StudentStatisticsPartEntity root,
        StudentStatisticsOnDiscipline part
    )
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        DisciplineName = part.DisciplineName.Name;
    }
}
