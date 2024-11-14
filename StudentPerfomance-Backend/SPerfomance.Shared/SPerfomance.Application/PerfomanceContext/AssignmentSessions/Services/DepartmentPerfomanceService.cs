using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public class DepartmentPerfomanceService
{
    private readonly List<AssignmentWeekDTO> _weeks;

    public DepartmentPerfomanceService(List<AssignmentWeekDTO> weeks) => _weeks = weeks;

    public async Task<List<AssignmentSessionDepartmentPerfomanceDTO>> CalculateForDepartments()
    {
        List<AssignmentSessionDepartmentPerfomanceDTO> perfomances = [];

        List<string> departmentNames = [];
        foreach (AssignmentWeekDTO week in _weeks)
        {
            foreach (AssignmentDTO assignment in week.Assignments)
            {
                if (departmentNames.Contains(assignment.DepartmentName))
                    continue;

                departmentNames.Add(assignment.DepartmentName);
            }
        }

        foreach (string department in departmentNames)
        {
            AssignmentSessionDepartmentPerfomanceDTO perfomance =
                new AssignmentSessionDepartmentPerfomanceDTO();
            perfomance.DepartmentName = department;
            int totals = 0;
            double averageMarksSum = 0;
            double averagePerfomanceSum = 0;
            foreach (var week in _weeks)
            {
                IEnumerable<AssignmentDTO> departmentAssignments = week.Assignments.Where(a =>
                    a.DepartmentName == department
                );

                totals += departmentAssignments.Count();

                foreach (AssignmentDTO assignment in departmentAssignments)
                {
                    averageMarksSum += assignment.AssignmentAverage;
                    averagePerfomanceSum += assignment.AssignmentPerfomance;
                }
            }

            perfomance.Average = Math.Round((double)averageMarksSum / totals, 3);
            perfomance.Perfomance = Math.Round((double)averagePerfomanceSum / totals, 3);
            perfomances.Add(perfomance);
        }

        return await Task.FromResult(perfomances);
    }
}
