namespace SPerfomance.Domain.Models.EducationDirections.Abstractions;

public interface IEducationDirectionRepository
{
    public Task Insert(EducationDirection direction);

    public Task Remove(EducationDirection direction);

    public Task Update(EducationDirection direction);

    public Task<EducationDirection?> Get(string code, string name, string type);

    public Task<EducationDirection?> GetByCode(string code);

    public Task<IReadOnlyCollection<EducationDirection>> GetAll();

    public Task<IReadOnlyCollection<EducationDirection>> GetPaged(int page, int pageSize);

    public Task<IReadOnlyCollection<EducationDirection>> GetFiltered(
        string? code,
        string? name,
        string? type
    );

    public Task<IReadOnlyCollection<EducationDirection>> GetPagedFiltered(
        string? code,
        string? name,
        string? type,
        int page,
        int pageSize
    );

    public Task<bool> Has(string code, string name, string type);

    public Task<bool> HasWithCode(string code);

    public Task<int> Count();

    public Task<int> GenerateEntityNumber();
}
