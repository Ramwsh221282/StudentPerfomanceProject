using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed record DisciplinesStatisticsReportDTO
{
    public Guid Id { get; init; }
    public string DisciplineName { get; init; }
    public string TeacherName { get; init; }
    public string TeacherSurname { get; init; }
    public string? TeacherPatronymic { get; init; }
    public double Average { get; init; }
    public double Perfomance { get; init; }
    public StudentStatisticsReportDTO[] Parts { get; init; }

    public DisciplinesStatisticsReportDTO(DisciplinesStatisticsReportEntity discipline)
    {
        Id = discipline.Id;
        DisciplineName = discipline.DisciplineName;
        TeacherName = discipline.TeacherName;
        TeacherSurname = discipline.TeacherSurname;
        TeacherPatronymic = discipline.TeacherPatronymic;
        Average = Math.Round(discipline.Average, 2);
        Perfomance = Math.Round(discipline.Perfomance, 2);
        Parts = InitializeStudentStatisticsReportArray(discipline.Parts);
    }

    private StudentStatisticsReportDTO[] InitializeStudentStatisticsReportArray(
        List<StudentStatisticsReportEntity> students
    )
    {
        int count = students.Count;
        if (count == 0)
            return Array.Empty<StudentStatisticsReportDTO>();
        StudentStatisticsReportDTO[] parts = new StudentStatisticsReportDTO[count];
        int left = 0;
        int right = count - 1;
        while (left <= right)
        {
            parts[left] = new StudentStatisticsReportDTO(students[left]);
            left++;
            if (left <= right)
            {
                parts[right] = new StudentStatisticsReportDTO(students[right]);
                right--;
            }
        }
        return parts;
    }
}
