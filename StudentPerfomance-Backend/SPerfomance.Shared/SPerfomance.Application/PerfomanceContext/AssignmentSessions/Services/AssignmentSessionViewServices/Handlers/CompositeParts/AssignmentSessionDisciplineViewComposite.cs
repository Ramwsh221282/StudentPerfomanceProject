using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers.CompositeParts;

public class AssignmentSessionDisciplineViewComposite(
    AssignmentSessionView view,
    AssignmentSession session
) : ICompositeAssignmentView
{
    private readonly AssignmentSessionView _view = view;
    private readonly AssignmentSession _session = session;

    public AssignmentSessionView CreateView()
    {
        foreach (var weekView in _view.Weeks)
        {
            Assignment[] assignments = _session
                .Weeks.SelectMany(a => a.Assignments)
                .Where(a => a.Week.Group.Name == weekView.GroupName)
                .ToArray();
            weekView.Disciplines = assignments
                .Select(a => new AssignmentDisciplineView(a))
                .ToArray();
        }
        return _view;
    }
}
