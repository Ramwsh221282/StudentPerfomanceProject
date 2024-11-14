using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.TeacherJournals;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

public class TeacherAssignmentSession
{
    private List<TeacherJournal> _journals = [];

    public TeacherName Teacher { get; init; }

    public TeacherAssignmentSession(Teacher teacher, AssignmentSession session)
    {
        Teacher = teacher.Name;
        foreach (AssignmentWeek week in session.Weeks)
        {
            TeacherJournal journal = new TeacherJournal();
            journal = journal.SetGroupName(week.Group!);
            Assignment[] assignments = week
                .Assignments.Where(a => a.Discipline.Teacher!.Name == teacher.Name)
                .ToArray();
            foreach (Assignment assignment in assignments)
            {
                TeacherJournalDiscipline discipline = new TeacherJournalDiscipline(
                    assignment.Discipline.Discipline
                );
                foreach (StudentAssignment student in assignment.StudentAssignments)
                {
                    discipline.AppendStudent(student, week.Group.Name.Name);
                }
                journal.SetDiscipline(discipline);
            }
            _journals.Add(journal);
        }
    }

    public List<TeacherJournal> Journals => _journals;
}
