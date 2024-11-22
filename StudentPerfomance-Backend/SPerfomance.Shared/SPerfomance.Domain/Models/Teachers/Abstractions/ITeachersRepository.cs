namespace SPerfomance.Domain.Models.Teachers.Abstractions;

public interface ITeachersRepository
{
    public Task Insert(Teacher entity, CancellationToken ct = default);

    public Task Remove(Teacher entity, CancellationToken ct = default);

    public Task Update(Teacher entity, CancellationToken ct = default);

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);

    public Task<Teacher?> GetByName(
        string name,
        string surname,
        string? patronymic,
        CancellationToken ct = default
    );

    public Task<Teacher?> Get(
        string name,
        string surname,
        string patronymic,
        string jobTitle,
        string workingState,
        CancellationToken ct = default
    );
}
