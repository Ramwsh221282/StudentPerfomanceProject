using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Domain.Models.EducationPlans.Abstractions;

public interface IEducationPlansRepository
{
    public Task Insert(EducationPlan entity, CancellationToken ct = default);

    public Task Remove(EducationPlan entity, CancellationToken ct = default);

    public Task Update(EducationPlan entity, CancellationToken ct = default);

    public Task<bool> HasPlan(
        EducationDirection direction,
        int year,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<EducationPlan>> GetFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<EducationPlan>> GetPagedFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year,
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<EducationPlan>> GetAll(CancellationToken ct = default);

    public Task<IReadOnlyCollection<EducationPlan>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<int> Count(CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);
}
