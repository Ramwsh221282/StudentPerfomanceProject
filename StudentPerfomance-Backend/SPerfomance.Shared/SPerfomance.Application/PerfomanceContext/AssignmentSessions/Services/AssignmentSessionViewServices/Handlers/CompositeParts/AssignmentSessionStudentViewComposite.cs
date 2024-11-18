using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers.CompositeParts;

public class AssignmentSessionStudentViewComposite(
    AssignmentSessionView view,
    AssignmentSession session
) : ICompositeAssignmentView
{
    private readonly AssignmentSessionView _view = view;
    private readonly AssignmentSession _session = session;

    public AssignmentSessionView CreateView()
    {
        foreach (var week in _view.Weeks)
        {
            foreach (var discipline in week.Disciplines)
            {
                StudentAssignment[] studentAssignments = _session
                    .Weeks.SelectMany(w => w.Assignments)
                    .SelectMany(s => s.StudentAssignments)
                    .Where(sa => sa.Assignment.Discipline.Discipline == discipline.Discipline)
                    .ToArray();
                discipline.Students = studentAssignments
                    .Select(sa => new AssignmentStudentView(sa))
                    .ToArray();
            }
        }
        return _view;
    }
}
