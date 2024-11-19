using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Api.Features.PerfomanceContext.Responses;

public sealed class StudentStatisticsReportDTO(StudentStatisticsReportEntity student)
{
    public Guid Id { get; init; } = student.Id;
    public string StudentName { get; init; } = student.StudentName;
    public string StudentSurname { get; init; } = student.StudentSurname;
    public string? StudentPatronymic { get; init; } = student.StudentPatronymic;
    public double Average { get; init; } = Math.Round(student.Average, 2);
    public double Perfomance { get; init; } = Math.Round(student.Perfomance, 2);
    public string Grade { get; init; } = student.Grade;
    public ulong Recordbook { get; init; } = student.Recordbook;
}
