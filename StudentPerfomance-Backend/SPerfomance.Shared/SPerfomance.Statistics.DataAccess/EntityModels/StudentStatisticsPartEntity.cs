using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class StudentStatisticsPartEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public GroupStatisticsReportEntity Root { get; init; }

    public string StudentName { get; init; }

    public string StudentSurname { get; init; }

    public string? StudentPatronymic { get; init; }

    public ulong StudentRecordBook { get; init; }

    public string Perfomance { get; init; }

    public string Average { get; init; }

    public List<StudentStatisticsOnDisciplinePartEntity> Parts = [];

    private StudentStatisticsPartEntity()
    {
        Root = null!;
        StudentName = string.Empty;
        StudentSurname = string.Empty;
        StudentPatronymic = string.Empty;
    }

    public StudentStatisticsPartEntity(GroupStatisticsReportEntity root, StudentStatisticsPart part)
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        StudentName = part.Name.Name;
        StudentSurname = part.Name.Surname;
        StudentPatronymic = part.Name.Patronymic;
        StudentRecordBook = part.Recordbook.Recordbook;
        Perfomance = part.Perfomance.ToString("F2");
        Average = part.Average.ToString("F0");
        foreach (var discipline in part.StudentStatisticsOnDisciplines)
        {
            Parts.Add(new StudentStatisticsOnDisciplinePartEntity(this, discipline));
        }
    }
}
