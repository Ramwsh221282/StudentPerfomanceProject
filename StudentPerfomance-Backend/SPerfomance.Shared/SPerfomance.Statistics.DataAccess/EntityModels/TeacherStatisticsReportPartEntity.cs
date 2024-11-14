using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports.Parts;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class TeacherStatisticsReportPartEntity
{
    public Guid Id { get; init; }

    public Guid RootId { get; init; }

    public DepartmentStatisticsReportPartEntity Root { get; init; }

    public string TeacherName { get; init; }

    public string TeacherSurname { get; init; }

    public string TeacherPatronymic { get; init; }

    public string Average { get; init; }

    public string Perfomance { get; init; }

    private TeacherStatisticsReportPartEntity()
    {
        Root = null!;
        TeacherName = string.Empty;
        TeacherSurname = string.Empty;
        TeacherPatronymic = string.Empty;
    }

    public TeacherStatisticsReportPartEntity(
        DepartmentStatisticsReportPartEntity root,
        TeacherStatisticsReportPart part
    )
        : this()
    {
        Id = part.Id;
        RootId = root.Id;
        Root = root;
        TeacherName = part.TeacherName.Name;
        TeacherSurname = part.TeacherName.Surname;
        TeacherPatronymic = part.TeacherName.Patronymic;
        Average = part.Average.ToString("F3");
        Perfomance = part.Perfomance.ToString("F3");
    }
}
