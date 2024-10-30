namespace SPerfomance.Domain.Models.SemesterPlans.Abstractions;

public interface ISemesterPlansRepository
{
	public Task Insert(SemesterPlan entity);

	public Task Remove(SemesterPlan entity);

	public Task Update(SemesterPlan entity);

	public Task DeattachTeacherId(SemesterPlan entity);

	public Task AttachTeacherId(SemesterPlan entity);

	public Task<int> GenerateEntityNumber();
}
