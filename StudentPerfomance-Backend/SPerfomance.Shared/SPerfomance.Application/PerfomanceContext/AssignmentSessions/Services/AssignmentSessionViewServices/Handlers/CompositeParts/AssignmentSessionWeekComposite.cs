using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers.CompositeParts;

public class AssignmentSessionWeekComposite(AssignmentSessionView view, AssignmentSession session)
    : ICompositeAssignmentView
{
    public AssignmentSessionView CreateView()
    {
        view.Weeks = session.Weeks.Select(w => new AssignmentWeekView(w)).ToArray();
        foreach (var week in view.Weeks)
        {
            Assignment[] assignments = session
                .Weeks.SelectMany(w => w.Assignments)
                .Where(a => a.Week.Group.Name == week.GroupName)
                .ToArray();
            week.Disciplines = assignments.Select(a => new AssignmentDisciplineView(a)).ToArray();
        }

        return view;
    }
}
