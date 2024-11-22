namespace SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

public interface ITeacherDepartmentsRepository
{
    public Task Insert(TeachersDepartments entity, CancellationToken ct = default);

    public Task Remove(TeachersDepartments entity, CancellationToken ct = default);

    public Task Update(TeachersDepartments entity, CancellationToken ct = default);

    public Task<TeachersDepartments?> Get(string name, CancellationToken ct = default);

    public Task<IReadOnlyCollection<TeachersDepartments>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<TeachersDepartments>> GetPagedFiltered(
        string? name,
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<TeachersDepartments>> GetAll(CancellationToken ct = default);

    public Task<IReadOnlyCollection<TeachersDepartments>> GetFiltered(
        string? name,
        CancellationToken ct = default
    );

    public Task<bool> HasWithName(string name, CancellationToken ct = default);

    public Task<int> Count(CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);
}
