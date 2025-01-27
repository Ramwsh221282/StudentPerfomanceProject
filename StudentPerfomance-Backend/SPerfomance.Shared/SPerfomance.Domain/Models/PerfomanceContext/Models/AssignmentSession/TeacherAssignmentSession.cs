using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;

public class TeacherAssignmentSession
{
    private List<TeacherJournal> _journals = [];

    public TeacherName Teacher { get; init; }

    public TeacherAssignmentSession(
        Teacher teacher,
        Models.AssignmentSession.AssignmentSession session
    )
    {
        Teacher = teacher.Name;
        foreach (AssignmentWeek week in session.Weeks)
        {
            Assignment[] assignments = week
                .Assignments.Where(a => a.Discipline.Teacher!.Name == teacher.Name)
                .ToArray();
            if (assignments.Length == 0)
                continue;
            TeacherJournal journal = new TeacherJournal();
            journal = journal.SetGroupName(week.Group!);
            foreach (Assignment assignment in assignments)
            {
                TeacherJournalDiscipline discipline = new TeacherJournalDiscipline(
                    assignment.Discipline.Discipline
                );
                foreach (
                    StudentAssignment student in assignment.StudentAssignments.OrderBy(s =>
                        s.Student.Name.Surname
                    )
                )
                {
                    discipline.AppendStudent(student, week.Group.Name.Name);
                }
                journal.SetDiscipline(discipline);
            }
            _journals.Add(journal);
        }
        _journals = _journals.OrderBy(j => j.GroupName!.Name).ToList();
    }

    public List<TeacherJournal> Journals => _journals;
}
