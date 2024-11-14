using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Domain.Models.Users.Abstractions;

public interface IUsersRepository
{
    public Task Insert(User entity);

    public Task Remove(User entity);

    public Task Update(User entity);

    public Task<User?> GetByEmail(string? email);

    public Task<User?> GetById(string id);

    public Task<bool> HasWithEmail(string? email);

    public Task<IReadOnlyCollection<User>> GetFiltered(
        string? name,
        string? surname,
        string? patronymic,
        string? email,
        string? role,
        int page,
        int pageSize
    );

    public Task<IReadOnlyCollection<User>> GetPaged(int page, int pageSize);

    public Task<int> GenerateEntityNumber();

    public Task UpdateLoginDate(User entity);

    Task<Teacher?> GetTeacherByUser(User user);

    public Task<int> Count();
}
