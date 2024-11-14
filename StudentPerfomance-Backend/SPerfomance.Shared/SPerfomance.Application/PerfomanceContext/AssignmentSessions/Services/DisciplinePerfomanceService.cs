using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public sealed class DisciplinePerfomanceService
{
    private readonly Assignment _assignment;

    public DisciplinePerfomanceService(Assignment assignment)
    {
        _assignment = assignment;
    }

    public double CalculateAssignmentAverage()
    {
        int totals = 0;
        int marks = 0;

        foreach (StudentAssignment studentAssignment in _assignment.StudentAssignments)
        {
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

    public double CalculateAssignmentPerfomance()
    {
        int totals = 0;
        int positives = 0;

        foreach (StudentAssignment studentAssignment in _assignment.StudentAssignments)
        {
            if (studentAssignment.Value == AssignmentValue.Empty)
                continue;

            if (studentAssignment.Value == AssignmentValue.NotAttestated)
            {
                totals += 1;
                continue;
            }

            if (studentAssignment.Value == AssignmentValue.Bad)
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
