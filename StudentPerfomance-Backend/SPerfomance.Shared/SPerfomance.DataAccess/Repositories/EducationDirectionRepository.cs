using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class EducationDirectionRepository : IEducationDirectionRepository
{
    private readonly DatabaseContext _context = new();

    public async Task Insert(EducationDirection direction, CancellationToken ct = default)
    {
        direction.SetNumber(await GenerateEntityNumber(ct));
        await _context.EducationDirections.AddAsync(direction, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.EducationDirections.CountAsync(ct);

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .EducationDirections.Select(s => s.EntityNumber)
            .ToArrayAsync(ct);
        return numbers.GetOrderedValue();
    }

    public async Task<IReadOnlyCollection<EducationDirection>> GetAll(
        CancellationToken ct = default
    ) =>
        await _context
            .EducationDirections.OrderByDescending(d => d.EntityNumber)
            .Include(d => d.Plans)
            .ThenInclude(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(ct);

    public async Task<IReadOnlyCollection<EducationDirection>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .EducationDirections.OrderBy(d => d.EntityNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(d => d.Plans)
            .ThenInclude(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(ct);

    public async Task Remove(EducationDirection direction, CancellationToken ct = default) =>
        await _context.EducationDirections.Where(d => d.Id == direction.Id).ExecuteDeleteAsync(ct);

    public async Task Update(EducationDirection direction, CancellationToken ct = default)
    {
        await _context
            .EducationDirections.Where(d => d.Id == direction.Id)
            .ExecuteUpdateAsync(
                d =>
                    d.SetProperty(d => d.Name.Name, direction.Name.Name)
                        .SetProperty(d => d.Type.Type, direction.Type.Type)
                        .SetProperty(d => d.Code.Code, direction.Code.Code),
                cancellationToken: ct
            );
    }

    public async Task<EducationDirection?> Get(
        string? code,
        string? name,
        string? type,
        CancellationToken ct = default
    ) =>
        await _context
            .EducationDirections.Include(d => d.Plans)
            .ThenInclude(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(
                d => d.Code.Code == code && d.Name.Name == name && d.Type.Type == type,
                ct
            );

    public async Task<EducationDirection?> GetByCode(string code, CancellationToken ct = default) =>
        await _context
            .EducationDirections.Include(d => d.Plans)
            .ThenInclude(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(d => d.Code.Code == code, ct);

    public async Task<IReadOnlyCollection<EducationDirection>> GetFiltered(
        string? code,
        string? name,
        string? type,
        CancellationToken ct = default
    ) =>
        await _context
            .EducationDirections.OrderBy(d => d.EntityNumber)
            .Where(d =>
                !string.IsNullOrWhiteSpace(code) && d.Code.Code.Contains(code)
                || !string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name)
                || !string.IsNullOrWhiteSpace(type) && d.Type.Type == type
            )
            .Include(d => d.Plans)
            .ThenInclude(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(ct);

    public async Task<IReadOnlyCollection<EducationDirection>> GetPagedFiltered(
        string? code,
        string? name,
        string? type,
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .EducationDirections.OrderBy(d => d.EntityNumber)
            .Where(d =>
                !string.IsNullOrWhiteSpace(code) && d.Code.Code.Contains(code)
                || !string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name)
                || !string.IsNullOrWhiteSpace(type) && d.Type.Type == type
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(d => d.Plans)
            .ThenInclude(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(ct);

    public async Task<bool> Has(
        string code,
        string name,
        string type,
        CancellationToken ct = default
    ) =>
        await _context.EducationDirections.AnyAsync(
            d => d.Code.Code == code && d.Name.Name == name && d.Type.Type == type,
            ct
        );

    public async Task<bool> HasWithCode(string code, CancellationToken ct = default) =>
        await _context.EducationDirections.AnyAsync(d => d.Code.Code == code, ct);
}
