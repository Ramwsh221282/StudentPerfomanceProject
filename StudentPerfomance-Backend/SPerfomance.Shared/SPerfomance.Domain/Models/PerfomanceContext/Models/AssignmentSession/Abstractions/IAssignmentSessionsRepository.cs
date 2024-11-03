namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

public interface IAssignmentSessionsRepository
{
	public Task Insert(AssignmentSession session);

	public Task Remove(AssignmentSession session);

	public Task Update(AssignmentSession session);

	public Task<IReadOnlyCollection<AssignmentSession>> GetPaged(int page, int pageSize);

	public Task<IReadOnlyCollection<AssignmentSession>> GetInPeriodPaged(DateTime startDate, DateTime endDate, int page, int pageSize);

	public Task GetByPeriod(DateTime startDate, DateTime endDate);

	public Task<int> GenerateEntityNumber();

	public void DoBackgroundWork();

	public Task<int> Count();
}
