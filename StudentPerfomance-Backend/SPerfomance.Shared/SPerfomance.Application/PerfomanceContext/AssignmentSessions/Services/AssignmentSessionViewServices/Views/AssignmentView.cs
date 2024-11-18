using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.ValueObjects;
using SPerfomance.Domain.Models.StudentGroups.ValueObjects;
using SPerfomance.Domain.Models.Students.ValueObjects;
using SPerfomance.Domain.Models.Teachers.ValueObjects;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;

// Представление контрольной недели
public sealed class AssignmentSessionView(AssignmentSession session)
{
    public Guid Id { get; init; } = session.Id;
    public int Number { get; init; } = session.EntityNumber;
    public string StartDate { get; init; } = session.SessionStartDate.ToString("dd/MM/yyyy");
    public string EndDate { get; init; } = session.SessionCloseDate.ToString("dd/MM/yyyy");
    public string State { get; init; } =
        session.State.State switch
        {
            true => "Открыта",
            false => "Закрыта",
        };

    public AssignmentWeekView[] Weeks { get; set; } = Array.Empty<AssignmentWeekView>();
}

// Представление успеваемости в группе
public sealed class AssignmentWeekView(AssignmentWeek week)
{
    public Guid Id { get; set; } = week.Id;
    public StudentGroupName GroupName { get; init; } = week.Group.Name;
    public DirectionCode Code { get; init; } = week.Group.EducationPlan!.Direction.Code;
    public DirectionType Type { get; init; } = week.Group.EducationPlan!.Direction.Type;
    public Course Course { get; init; } = Course.CreateFromGroup(week.Group);
    public AssignmentDisciplineView[] Disciplines { get; set; } =
        Array.Empty<AssignmentDisciplineView>();
    public double Average { get; set; }
    public double Perfomance { get; set; }

    public void CalculateAverage()
    {
        int totalMarks = 0;
        int sumMarks = 0;
        foreach (var discipline in Disciplines)
        {
            foreach (var student in discipline.Students)
            {
                if (student.Grade.Value == 1)
                    continue;

                if (student.Grade.Value == 0)
                {
                    totalMarks++;
                    sumMarks += 2;
                    continue;
                }

                totalMarks++;
                sumMarks += student.Grade.Value;
            }
        }

        if (totalMarks == 0)
            return;

        Average = (double)sumMarks / totalMarks;
    }

    public void CalculatePerfomance()
    {
        int totalMarks = 0;
        int totalPositive = 0;
        foreach (var discipline in Disciplines)
        {
            foreach (var student in discipline.Students)
            {
                if (student.Grade.Value == 1)
                    continue;

                if (student.Grade.Value == 2 || student.Grade.Value == 0)
                {
                    totalMarks++;
                    continue;
                }

                totalMarks++;
                totalPositive += 1;
            }
        }

        if (totalMarks == 0)
            return;

        Perfomance = ((double)totalPositive / totalMarks) * 100;
    }
}

// Представление дисциплины
public sealed class AssignmentDisciplineView(Assignment assignment)
{
    public Guid Id { get; init; } = assignment.Id;
    public TeacherName TeacherName { get; init; } = assignment.Discipline.Teacher!.Name;
    public DisciplineName Discipline { get; init; } = assignment.Discipline.Discipline;
    public double Average { get; private set; }
    public double Perfomance { get; private set; }
    public AssignmentStudentView[] Students { get; set; } =
        assignment.StudentAssignments.Select(a => new AssignmentStudentView(a)).ToArray();

    public void CalculateAverage()
    {
        Span<byte> grades = Students.Select(s => s.Grade.Value).ToArray();
        if (grades.Length == 0)
            return;

        if (grades.Length == 0)
            return;

        int totalMarks = 0;
        int marksSum = 0;
        for (int i = 0; i < grades.Length; i++)
        {
            if (grades[i] == 1)
                continue;

            if (grades[i] == 0)
            {
                totalMarks += 1;
                marksSum += 2;
                continue;
            }

            totalMarks += 1;
            marksSum += grades[i];
        }

        if (totalMarks == 0)
            return;

        Average = (double)marksSum / totalMarks;
    }

    public void CalculatePerfomance()
    {
        Span<byte> grades = Students.Select(s => s.Grade.Value).ToArray();
        if (grades.Length == 0)
            return;

        if (grades.Length == 0)
            return;

        int totalMarks = 0;
        int positiveMarks = 0;
        for (int i = 0; i < grades.Length; i++)
        {
            if (grades[i] == 1)
                continue;

            if (grades[i] == 2 || grades[i] == 0)
            {
                totalMarks += 1;
                continue;
            }

            totalMarks += 1;
            positiveMarks += 1;
        }

        if (totalMarks == 0)
            return;

        Perfomance = ((double)positiveMarks / totalMarks) * 100;
    }
}

// Представление успеваемости студента
public sealed class AssignmentStudentView(StudentAssignment assignment)
{
    public Guid Id { get; init; } = assignment.Id;
    public string Value { get; init; } = assignment.Value;
    public StudentName Name { get; init; } = assignment.Student.Name;
    public StudentRecordbook Recordbook { get; init; } = assignment.Student.Recordbook;
    public AssignmentValue Grade { get; init; } = assignment.Value;
    public DisciplineName AssignmentDiscipline = assignment.Assignment.Discipline.Discipline;
    public double Average { get; private set; }
    public double Perfomance { get; private set; }
    public StudentGroupName GroupName { get; init; } = assignment.Assignment.Week.Group.Name;

    public void CalculateAverage(AssignmentWeekView view)
    {
        Span<byte> grades = view
            .Disciplines.SelectMany(d => d.Students)
            .Where(s => s.Recordbook == Recordbook)
            .Select(s => s.Grade.Value)
            .ToArray();

        if (grades.Length == 0)
            return;

        int totalMarks = 0;
        int marksSum = 0;
        for (int i = 0; i < grades.Length; i++)
        {
            if (grades[i] == 1)
                continue;

            if (grades[i] == 0)
            {
                totalMarks += 1;
                marksSum += 2;
                continue;
            }

            totalMarks += 1;
            marksSum += grades[i];
        }

        if (totalMarks == 0)
            return;

        Average = (double)marksSum / totalMarks;
    }

    public void CalculatePerfomance(AssignmentWeekView view)
    {
        Span<byte> grades = view
            .Disciplines.SelectMany(d => d.Students)
            .Where(s => s.Recordbook == Recordbook)
            .Select(s => s.Grade.Value)
            .ToArray();

        if (grades.Length == 0)
            return;

        int totalMarks = 0;
        int positiveMarks = 0;
        for (int i = 0; i < grades.Length; i++)
        {
            if (grades[i] == 1)
                continue;

            if (grades[i] == 2 || grades[i] == 0)
            {
                totalMarks += 1;
                continue;
            }

            totalMarks += 1;
            positiveMarks += 1;
        }

        if (totalMarks == 0)
            return;

        Perfomance = ((double)positiveMarks / totalMarks) * 100;
    }
}
