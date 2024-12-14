using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers.CompositeParts;

public class AssignmentSessionViewComposite(AssignmentSession session) : ICompositeAssignmentView
{
    private readonly AssignmentSessionView _view = new(session);

    public AssignmentSessionView CreateView()
    {
        return _view;
    }
}
