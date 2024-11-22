namespace SPerfomance.Domain.Models.EducationDirections.Abstractions;

public interface IEducationDirectionRepository
{
    public Task Insert(EducationDirection direction, CancellationToken ct = default);

    public Task Remove(EducationDirection direction, CancellationToken ct = default);

    public Task Update(EducationDirection direction, CancellationToken ct = default);

    public Task<EducationDirection?> Get(
        string code,
        string name,
        string type,
        CancellationToken ct = default
    );

    public Task<EducationDirection?> GetByCode(string code, CancellationToken ct = default);

    public Task<IReadOnlyCollection<EducationDirection>> GetAll(CancellationToken ct = default);

    public Task<IReadOnlyCollection<EducationDirection>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<EducationDirection>> GetFiltered(
        string? code,
        string? name,
        string? type,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<EducationDirection>> GetPagedFiltered(
        string? code,
        string? name,
        string? type,
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<bool> Has(string code, string name, string type, CancellationToken ct = default);

    public Task<bool> HasWithCode(string code, CancellationToken ct = default);

    public Task<int> Count(CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);
}
