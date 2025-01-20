using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.Users.GetAdminShortInfoFeature.Responses;

public sealed record StudentGroupAttachedSemester(int Semester);

public sealed record StudentGroupStudent(
    string Name,
    string Surname,
    string? Patronymic,
    string State,
    ulong Recordbook
);

public class StudentGroupShortInfoResponse
{
    public Guid Id { get; }
    public string Name { get; }
    public StudentGroupAttachedSemester? Semester { get; }
    public bool HasSemester { get; }
    private List<StudentGroupStudent> Students { get; } = [];
    public bool HasStudents { get; }

    public StudentGroupShortInfoResponse(StudentGroup group)
    {
        Id = group.Id;
        Name = group.Name.Name;
        if (group.ActiveGroupSemester != null)
            Semester = new StudentGroupAttachedSemester(group.ActiveGroupSemester.Number.Number);
        foreach (var student in group.Students)
            Students.Add(
                new StudentGroupStudent(
                    student.Name.Name,
                    student.Name.Surname,
                    student.Name.Patronymic,
                    student.State.State,
                    student.Recordbook.Recordbook
                )
            );
        HasStudents = Students.Any();
        HasSemester = Semester != null;
    }
}
