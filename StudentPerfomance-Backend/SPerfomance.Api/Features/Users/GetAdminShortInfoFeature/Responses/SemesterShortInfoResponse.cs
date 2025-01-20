using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Api.Features.Users.GetAdminShortInfoFeature.Responses;

public record SemesterDiscipline(
    Guid Id,
    string Discipline,
    DisciplineTeacher? Teacher,
    bool HasTeacher
);

public record DisciplineTeacher(
    Guid Id,
    string Name,
    string Surname,
    string? Patronymic,
    TeacherDepartment Department
);

public record TeacherDepartment(Guid Id, string Name);

public class SemesterShortInfoResponse
{
    public Guid Id { get; }
    public int Number { get; }
    public List<SemesterDiscipline> Disciplines { get; } = [];
    public bool HasDisciplines { get; }
    public bool HasDisciplinesWithoutTeacher { get; }
    public List<SemesterDiscipline> DisciplinesWithoutTeacher { get; } = [];

    public SemesterShortInfoResponse(Semester semester)
    {
        Id = semester.Id;
        Number = semester.Number.Number;
        foreach (var discipline in semester.Disciplines)
            Disciplines.Add(BuildDiscipline(discipline));
        HasDisciplines = Disciplines.Any();
        HasDisciplinesWithoutTeacher = Disciplines.Any(d => !d.HasTeacher);
        DisciplinesWithoutTeacher = Disciplines
            .Where(d => !d.HasTeacher)
            .OrderBy(d => d.Discipline)
            .ToList();
        Disciplines = Disciplines.OrderBy(d => d.Discipline).ToList();
    }

    private SemesterDiscipline BuildDiscipline(SemesterPlan plan)
    {
        Guid id = plan.Id;
        string name = plan.Discipline.Name;
        Teacher? teacher = plan.Teacher;
        if (teacher == null)
            return new SemesterDiscipline(id, name, null, false);
        TeacherDepartment department = new TeacherDepartment(
            teacher.Department.Id,
            teacher.Department.Name.Name
        );
        DisciplineTeacher teacherResponse = new DisciplineTeacher(
            teacher.Id,
            teacher.Name.Name,
            teacher.Name.Surname,
            teacher.Name.Patronymic,
            department
        );
        return new SemesterDiscipline(id, name, teacherResponse, true);
    }
}
