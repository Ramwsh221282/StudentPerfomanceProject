using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DatabaseContext _context = new();

    public async Task Insert(User entity, CancellationToken ct = default)
    {
        entity.SetNumber(await GenerateEntityNumber(ct));
        await _context.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.Users.CountAsync(cancellationToken: ct);

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Users.Select(u => u.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task<User?> GetById(string id, CancellationToken ct = default) =>
        await _context
            .Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id.ToString() == id.ToUpper(), cancellationToken: ct);

    public async Task<IReadOnlyCollection<User>> GetAll(CancellationToken ct = default) =>
        await _context
            .Users.AsNoTracking()
            .AsSplitQuery()
            .OrderBy(u => u.Name.Surname)
            .ToListAsync();

    public async Task<Teacher?> GetTeacherByUser(User user, CancellationToken ct = default) =>
        await _context.Teachers.FirstOrDefaultAsync(
            t =>
                t.Name.Name == user.Name.Name
                && t.Name.Surname == user.Name.Surname
                && t.Name.Patronymic == user.Name.Patronymic,
            cancellationToken: ct
        );

    public async Task<IReadOnlyCollection<User>> GetFiltered(
        string? name,
        string? surname,
        string? patronymic,
        string? email,
        string? role,
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .Users.Where(u =>
                !string.IsNullOrWhiteSpace(name) && u.Name.Name.Contains(name)
                || !string.IsNullOrWhiteSpace(surname) && u.Name.Surname.Contains(surname)
                || !string.IsNullOrWhiteSpace(patronymic) && u.Name.Patronymic.Contains(patronymic)
                || !string.IsNullOrWhiteSpace(email) && u.Email.Email.Contains(email)
                || !string.IsNullOrWhiteSpace(role) && u.Role.Role.Contains(role)
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<User>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .Users.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken: ct);

    public async Task Remove(User entity, CancellationToken ct = default) =>
        await _context
            .Users.Where(u => u.Id == entity.Id)
            .ExecuteDeleteAsync(cancellationToken: ct);

    public async Task Update(User entity, CancellationToken ct = default) =>
        await _context
            .Users.Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(
                u =>
                    u.SetProperty(u => u.Name.Name, entity.Name.Name)
                        .SetProperty(u => u.Name.Surname, entity.Name.Surname)
                        .SetProperty(u => u.Name.Patronymic, entity.Name.Patronymic)
                        .SetProperty(u => u.Email.Email, entity.Email.Email)
                        .SetProperty(u => u.Role.Role, entity.Role.Role)
                        .SetProperty(u => u.HashedPassword, entity.HashedPassword)
                        .SetProperty(u => u.AttachedRoleId, entity.AttachedRoleId),
                cancellationToken: ct
            );

    public async Task<User?> GetByEmail(string? email, CancellationToken ct = default) =>
        await _context
            .Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Email == email, cancellationToken: ct);

    public async Task<bool> HasWithEmail(string? email, CancellationToken ct = default) =>
        await _context.Users.AnyAsync(u => u.Email.Email == email, cancellationToken: ct);

    public async Task UpdateLoginDate(User entity, CancellationToken ct = default) =>
        await _context
            .Users.Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(
                u => u.SetProperty(u => u.LastLoginDate, entity.LastLoginDate),
                cancellationToken: ct
            );
}
