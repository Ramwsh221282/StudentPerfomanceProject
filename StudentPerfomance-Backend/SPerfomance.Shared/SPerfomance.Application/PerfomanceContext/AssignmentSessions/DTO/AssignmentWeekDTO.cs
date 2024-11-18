using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.Semesters.ValueObjects;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentWeekDTO
{
    public Guid Id { get; init; }
    public StudentGroupDTO? Group { get; init; }
    public string DirectionType { get; init; }
    public string DirectionCode { get; init; }
    public List<AssignmentDTO> Assignments { get; init; }
    public double AverageMarks { get; init; }
    public double AveragePerfomancePercent { get; init; }
    public int Course { get; init; }

    public AssignmentWeekDTO(AssignmentWeek week)
    {
        Id = week.Id;
        Group = week.Group.MapFromDomain();
        DirectionType = week.Group.EducationPlan!.Direction.Type.Type;
        DirectionCode = week.Group.EducationPlan!.Direction.Code.Code;

        Assignments = week.Assignments.Select(a => new AssignmentDTO(a)).ToList();
        foreach (var student in Assignments.SelectMany(assignment => assignment.StudentAssignments))
        {
            student.StudentAverage =
                StudentAssignmentPerfomanceService.CalculateStudentAverageInWeekAssignments(
                    week.Assignments,
                    student
                );

            student.StudentPerfomance =
                StudentAssignmentPerfomanceService.CalculateStudentPerfomanceInWeekAssignments(
                    week.Assignments,
                    student
                );
        }

        AssignmentWeekPerfomanceService service = new AssignmentWeekPerfomanceService(week);
        AverageMarks = service.CalculateWeekAverageAssignments();
        AveragePerfomancePercent = service.CalculateWeekAveragePerfomance();
        Course = SemesterNumber.EstimateGroupCourse(week.Group.ActiveGroupSemester!);
    }
}
