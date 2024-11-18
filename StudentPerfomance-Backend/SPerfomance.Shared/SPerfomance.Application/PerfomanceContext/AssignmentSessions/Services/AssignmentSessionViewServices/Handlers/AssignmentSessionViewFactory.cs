using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers.CompositeParts;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;

public sealed class AssignmentSessionViewFactory : ICompositeAssignmentView
{
    private readonly AssignmentSessionView _view;

    public AssignmentSessionViewFactory(AssignmentSession session)
    {
        _view = new AssignmentSessionViewComposite(session).CreateView();
        _view = new AssignmentSessionWeekComposite(_view, session).CreateView();
        CalculateDisciplineParameters().Wait();
    }

    private async Task CalculateDisciplineParameters()
    {
        foreach (var week in _view.Weeks)
        {
            foreach (var discipline in week.Disciplines)
            {
                discipline.CalculateAverage();
                discipline.CalculatePerfomance();

                List<Task> studentTasks = new List<Task>();
                foreach (var student in discipline.Students)
                {
                    studentTasks.Add(
                        Task.Run(() =>
                        {
                            student.CalculateAverage(week);
                        })
                    );
                    studentTasks.Add(
                        Task.Run(() =>
                        {
                            student.CalculatePerfomance(week);
                        })
                    );
                }

                await Task.WhenAll(studentTasks);
            }
            week.CalculateAverage();
            week.CalculatePerfomance();
        }
    }

    public AssignmentSessionView CreateView() => _view;
}
