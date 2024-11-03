using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.HostedServices;

public sealed class SessionChecker : IHostedService, IDisposable
{
	private readonly IServiceScopeFactory _factory;
	private readonly IAssignmentSessionsRepository _repository;
	private Timer _timer;

	public SessionChecker(IServiceScopeFactory factory)
	{
		_factory = factory;
		_repository = _factory
		.CreateScope()
		.ServiceProvider
		.GetRequiredService<IAssignmentSessionsRepository>();
	}

	public void Dispose()
	{
		_timer?.Dispose();
	}

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
		return Task.CompletedTask;
	}

	private void DoWork(object state)
	{
		_repository.DoBackgroundWork();
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_timer?.Change(Timeout.Infinite, 0);
		return Task.CompletedTask;
	}
}
