using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Domain.Models.EducationPlans.Abstractions;

public interface IEducationPlansRepository
{
    public Task Insert(EducationPlan entity);

    public Task Remove(EducationPlan entity);

    public Task Update(EducationPlan entity);

    public Task<bool> HasPlan(EducationDirection direction, int year);

    public Task<IReadOnlyCollection<EducationPlan>> GetFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year
    );

    public Task<IReadOnlyCollection<EducationPlan>> GetPagedFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year,
        int page,
        int pageSize
    );

    public Task<IReadOnlyCollection<EducationPlan>> GetAll();

    public Task<IReadOnlyCollection<EducationPlan>> GetPaged(int page, int pageSize);

    public Task<int> Count();

    public Task<int> GenerateEntityNumber();
}
