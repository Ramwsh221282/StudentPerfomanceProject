using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class DisciplinesStatisticsReportEntity
{
    public Guid Id { get; set; }
    public Guid RootId { get; set; }
    public GroupStatisticsReportEntity Root { get; set; } = null!;
    public string DisciplineName { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string TeacherSurname { get; set; } = string.Empty;
    public string? TeacherPatronymic { get; set; }
    public double Average { get; set; }
    public double Perfomance { get; set; }
    public List<StudentStatisticsReportEntity> Parts { get; set; } = [];

    public static List<DisciplinesStatisticsReportEntity> CreateReport(
        GroupStatisticsReportEntity root,
        AssignmentDisciplineView[] views
    )
    {
        List<DisciplinesStatisticsReportEntity> reports = [];
        foreach (var view in views)
        {
            DisciplinesStatisticsReportEntity report = new DisciplinesStatisticsReportEntity();
            report.Id = view.Id;
            report.RootId = root.Id;
            report.Root = root;
            report.DisciplineName = view.Discipline.Name;
            report.TeacherName = view.TeacherName.Name;
            report.TeacherSurname = view.TeacherName.Surname;
            report.TeacherPatronymic = view.TeacherName.Patronymic;
            report.Average = view.Average;
            report.Perfomance = view.Perfomance;
            report.Parts = StudentStatisticsReportEntity.CreateReport(report, view);
            reports.Add(report);
        }
        return reports;
    }
}
