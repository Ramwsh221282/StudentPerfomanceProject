using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public sealed class AssignmentWeekPerfomanceService
{
    private readonly AssignmentWeek _week;

    public AssignmentWeekPerfomanceService(AssignmentWeek week) => _week = week;

    public double CalculateWeekAverageAssignments()
    {
        int totals = 0;
        double averageValues = 0;
        foreach (Assignment assignment in _week.Assignments)
        {
            averageValues += CalculateAverageOfAssignment(assignment);
            totals += 1;
        }

        if (totals == 0)
            return 0.00;

        return Math.Round((double)averageValues / totals, 3);
    }

    public double CalculateWeekAveragePerfomance()
    {
        int totals = 0;
        double averageValues = 0;
        foreach (Assignment assignment in _week.Assignments)
        {
            averageValues += CalculatePerfomanceOfAssignment(assignment);
            totals += 1;
        }

        if (totals == 0)
            return 0.00;

        return Math.Round((double)averageValues / totals, 3);
    }

    private static double CalculateAverageOfAssignment(Assignment assignment)
    {
        int totals = 0;
        int markValues = 0;

        foreach (StudentAssignment studentAssignment in assignment.StudentAssignments)
        {
            if (studentAssignment.Value == AssignmentValue.Empty)
                continue;

            if (studentAssignment.Value == AssignmentValue.NotAttestated)
            {
                markValues += 2;
                totals += 1;
                continue;
            }

            markValues += studentAssignment.Value.Value;
            totals += 1;
        }

        if (totals == 0)
            return 0.00;

        return Math.Round((double)markValues / totals, 3);
    }

    private static double CalculatePerfomanceOfAssignment(Assignment assignment)
    {
        int total = 0;
        int positives = 0;
        foreach (StudentAssignment studentAssignment in assignment.StudentAssignments)
        {
            if (studentAssignment.Value == AssignmentValue.Empty)
                continue;

            if (studentAssignment.Value == AssignmentValue.Bad)
            {
                total += 1;
                continue;
            }

            if (studentAssignment.Value == AssignmentValue.NotAttestated)
            {
                total += 1;
                continue;
            }

            total += 1;
            positives += 1;
        }

        if (total == 0)
            return 0.00;

        double percentage = Math.Round((double)positives / total, 3);
        return percentage * 100;
    }
}
