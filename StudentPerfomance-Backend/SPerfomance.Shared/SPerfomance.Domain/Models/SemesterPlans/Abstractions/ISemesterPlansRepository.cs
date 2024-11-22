namespace SPerfomance.Domain.Models.SemesterPlans.Abstractions;

public interface ISemesterPlansRepository
{
    public Task Insert(SemesterPlan entity, CancellationToken ct = default);

    public Task Remove(SemesterPlan entity, CancellationToken ct = default);

    public Task Update(SemesterPlan entity, CancellationToken ct = default);

    public Task DeattachTeacherId(SemesterPlan entity, CancellationToken ct = default);

    public Task AttachTeacherId(SemesterPlan entity, CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);
}
