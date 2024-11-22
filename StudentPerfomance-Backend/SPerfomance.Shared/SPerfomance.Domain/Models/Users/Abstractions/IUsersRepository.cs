using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Domain.Models.Users.Abstractions;

public interface IUsersRepository
{
    public Task Insert(User entity, CancellationToken ct = default);

    public Task Remove(User entity, CancellationToken ct = default);

    public Task Update(User entity, CancellationToken ct = default);

    public Task<User?> GetByEmail(string? email, CancellationToken ct = default);

    public Task<User?> GetById(string id, CancellationToken ct = default);

    public Task<bool> HasWithEmail(string? email, CancellationToken ct = default);

    public Task<IReadOnlyCollection<User>> GetFiltered(
        string? name,
        string? surname,
        string? patronymic,
        string? email,
        string? role,
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<IReadOnlyCollection<User>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    );

    public Task<int> GenerateEntityNumber(CancellationToken ct = default);

    public Task UpdateLoginDate(User entity, CancellationToken ct = default);

    Task<Teacher?> GetTeacherByUser(User user, CancellationToken ct = default);

    public Task<int> Count(CancellationToken ct = default);
}
