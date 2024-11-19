using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;

namespace SPerfomance.Statistics.DataAccess.EntityModels;

public class StudentStatisticsReportEntity
{
    public Guid Id { get; set; }
    public Guid RootId { get; set; }
    public DisciplinesStatisticsReportEntity Root { get; set; } = null!;
    public string StudentName { get; set; } = string.Empty;
    public string StudentSurname { get; set; } = string.Empty;
    public string? StudentPatronymic { get; set; }
    public double Average { get; set; }
    public double Perfomance { get; set; }
    public string Grade { get; set; } = string.Empty;
    public ulong Recordbook { get; set; }

    public static List<StudentStatisticsReportEntity> CreateReport(
        DisciplinesStatisticsReportEntity root,
        AssignmentDisciplineView view
    )
    {
        List<StudentStatisticsReportEntity> reports = new List<StudentStatisticsReportEntity>();
        foreach (var student in view.Students)
        {
            StudentStatisticsReportEntity report = new StudentStatisticsReportEntity();
            report.Id = student.Id;
            report.RootId = root.Id;
            report.Root = root;
            report.StudentName = student.Name.Name;
            report.StudentSurname = student.Name.Surname;
            report.StudentPatronymic = student.Name.Patronymic;
            report.Average = student.Average;
            report.Perfomance = student.Perfomance;
            report.Grade = ParseGrade(student);
            report.Recordbook = student.Recordbook.Recordbook;
            reports.Add(report);
        }

        return reports;
    }

    private static string ParseGrade(AssignmentStudentView studentView)
    {
        return studentView.Grade.Value switch
        {
            0 => "НА",
            1 => "НП",
            2 => "2",
            3 => "3",
            4 => "4",
            5 => "5",
            _ => "НП",
        };
    }
}
