using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public static class StudentAssignmentPerfomanceService
{
    public static double CalculateStudentAverageInWeekAssignments(
        IReadOnlyCollection<Assignment> assignments,
        StudentAssignmentDTO student
    )
    {
        int totals = 0;
        int marks = 0;
        foreach (Assignment assignment in assignments)
        {
            StudentAssignment? studentAssignment = assignment.StudentAssignments.FirstOrDefault(
                sa => sa.Student.Recordbook.Recordbook == student.StudentRecordbook
            );

            if (studentAssignment == null)
                continue;

            if (studentAssignment.Value == AssignmentValue.Empty)
                continue;

            if (studentAssignment.Value == AssignmentValue.NotAttestated)
            {
                totals += 1;
                marks += 2;
                continue;
            }

            totals += 1;
            marks += studentAssignment.Value.Value;
        }

        if (totals == 0)
            return 0.00;

        return Math.Round((double)marks / totals, 3);
    }

    public static double CalculateStudentPerfomanceInWeekAssignments(
        IReadOnlyCollection<Assignment> assignments,
        StudentAssignmentDTO student
    )
    {
        int totals = 0;
        int positives = 0;

        foreach (Assignment assignment in assignments)
        {
            StudentAssignment? studentAssignment = assignment.StudentAssignments.FirstOrDefault(
                sa => sa.Student.Recordbook.Recordbook == student.StudentRecordbook
            );

            if (studentAssignment == null)
                continue;

            if (studentAssignment.Value == AssignmentValue.Empty)
                continue;

            if (studentAssignment.Value == AssignmentValue.Bad)
            {
                totals += 1;
                continue;
            }

            if (studentAssignment.Value == AssignmentValue.NotAttestated)
            {
                totals += 1;
                continue;
            }

            totals += 1;
            positives += 1;
        }

        if (totals == 0)
            return 0.00;

        double percentage = Math.Round((double)positives / totals, 3);
        return percentage * 100;
    }
}
