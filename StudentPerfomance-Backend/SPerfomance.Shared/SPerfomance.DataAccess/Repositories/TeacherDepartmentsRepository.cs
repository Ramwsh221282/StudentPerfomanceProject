using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class TeacherDepartmentsRepository : ITeacherDepartmentsRepository
{
    private readonly DatabaseContext _context = new();

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.Departments.CountAsync(ct);

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Departments.Select(d => d.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task<TeachersDepartments?> Get(string name, CancellationToken ct = default) =>
        await _context
            .Departments.Include(d => d.Teachers.OrderBy(t => t.Name.Surname))
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(t => t.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Groups)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Name.Name == name, cancellationToken: ct);

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetAll(
        CancellationToken ct = default
    ) =>
        await _context
            .Departments.OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers.OrderBy(t => t.Name.Surname))
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetFiltered(
        string? name,
        CancellationToken ct = default
    ) =>
        await _context
            .Departments.Where(d => !string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name))
            .OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers.OrderBy(t => t.Name.Surname))
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .Departments.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers.OrderBy(t => t.Name.Surname))
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetPagedFiltered(
        string? name,
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .Departments.Where(d => !string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers.OrderBy(t => t.Name.Surname))
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task<bool> HasWithName(string name, CancellationToken ct = default) =>
        await _context.Departments.AnyAsync(d => d.Name.Name == name, cancellationToken: ct);

    public async Task Insert(TeachersDepartments entity, CancellationToken ct = default)
    {
        entity.SetNumber(await GenerateEntityNumber(ct));
        await _context.Departments.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Remove(TeachersDepartments entity, CancellationToken ct = default)
    {
        await _context
            .Departments.Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .Where(d => d.Id == entity.Id)
            .ExecuteDeleteAsync(cancellationToken: ct);
    }

    public async Task Update(TeachersDepartments entity, CancellationToken ct = default) =>
        await _context
            .Departments.Where(d => d.Id == entity.Id)
            .ExecuteUpdateAsync(
                d => d.SetProperty(d => d.Name.Name, entity.Name.Name),
                cancellationToken: ct
            );
}
