using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public class CoursePerfomanceService
{
    private readonly List<AssignmentWeekDTO> _assignmentWeeks;

    private readonly List<AssignmentSessionCoursePerfomanceDTO> _coursePerfomances = [];

    public List<AssignmentSessionCoursePerfomanceDTO> CoursePerfomances => _coursePerfomances;

    public CoursePerfomanceService(List<AssignmentWeekDTO> assignmentWeeks)
    {
        _assignmentWeeks = assignmentWeeks;
        InitializeCourcePerfomance();
    }

    private void InitializeCourcePerfomance()
    {
        GetCoursePerfomancesForBachelor();
        GetCoursePerfomancesForMagister();
    }

    private void GetCoursePerfomancesForBachelor()
    {
        for (int course = 1; course <= 4; course++)
        {
            IEnumerable<AssignmentWeekDTO> courseWeeks = _assignmentWeeks.Where(w =>
                w.Course == course && w.DirectionType == "Бакалавриат"
            );

            if (courseWeeks.Count() == 0)
                continue;

            int totalWeeks = courseWeeks.Count();
            double totalAverage = 0;
            double totalPerfomance = 0;
            AssignmentSessionCoursePerfomanceDTO coursePerfomance =
                new AssignmentSessionCoursePerfomanceDTO();
            coursePerfomance.Course = course;

            foreach (AssignmentWeekDTO week in courseWeeks)
            {
                totalAverage += week.AverageMarks;
                totalPerfomance += week.AveragePerfomancePercent;
            }

            if (totalWeeks == 0)
            {
                coursePerfomance.Average = 0.00;
                coursePerfomance.Perfomance = 0.00;
                continue;
            }

            coursePerfomance.Average = Math.Round((double)totalAverage / totalWeeks, 3);
            coursePerfomance.Perfomance = Math.Round((double)totalPerfomance / totalWeeks, 3);
            coursePerfomance.DirectionType = "Бакалавриат";
            _coursePerfomances.Add(coursePerfomance);
        }
    }

    private void GetCoursePerfomancesForMagister()
    {
        for (int course = 1; course <= 2; course++)
        {
            IEnumerable<AssignmentWeekDTO> courseWeeks = _assignmentWeeks.Where(w =>
                w.Course == course && w.DirectionType == "Магистратура"
            );

            if (courseWeeks.Count() == 0)
                continue;

            int totalWeeks = courseWeeks.Count();
            double totalAverage = 0;
            double totalPerfomance = 0;
            AssignmentSessionCoursePerfomanceDTO coursePerfomance =
                new AssignmentSessionCoursePerfomanceDTO();
            coursePerfomance.Course = course;

            foreach (AssignmentWeekDTO week in courseWeeks)
            {
                totalAverage += week.AverageMarks;
                totalPerfomance += week.AveragePerfomancePercent;
            }

            if (totalWeeks == 0)
            {
                coursePerfomance.Average = 0.00;
                coursePerfomance.Perfomance = 0.00;
                continue;
            }

            coursePerfomance.Average = Math.Round((double)totalAverage / totalWeeks, 3);
            coursePerfomance.Perfomance = Math.Round((double)totalPerfomance / totalWeeks, 3);
            coursePerfomance.DirectionType = "Бакалавриат";
            _coursePerfomances.Add(coursePerfomance);
        }
    }
}
