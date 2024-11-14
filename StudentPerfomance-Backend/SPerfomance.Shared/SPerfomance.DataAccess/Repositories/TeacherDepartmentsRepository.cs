using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class TeacherDepartmentsRepository : ITeacherDepartmentsRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public async Task<int> Count() => await _context.Departments.CountAsync();

    public async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.Departments.Select(d => d.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public async Task<TeachersDepartments?> Get(string name) =>
        await _context
            .Departments.Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(t => t.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Groups)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Name.Name == name);

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetAll() =>
        await _context
            .Departments.OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetFiltered(string? name) =>
        await _context
            .Departments.Where(d => !string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name))
            .Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetPaged(int page, int pageSize) =>
        await _context
            .Departments.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();

    public async Task<IReadOnlyCollection<TeachersDepartments>> GetPagedFiltered(
        string? name,
        int page,
        int pageSize
    ) =>
        await _context
            .Departments.Where(d => !string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(d => d.EntityNumber)
            .Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task<bool> HasWithName(string name) =>
        await _context.Departments.AnyAsync(d => d.Name.Name == name);

    public async Task Insert(TeachersDepartments entity)
    {
        entity.SetNumber(await GenerateEntityNumber());
        await _context.Departments.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(TeachersDepartments entity)
    {
        await _context
            .Departments.Include(d => d.Teachers)
            .ThenInclude(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Direction)
            .Where(d => d.Id == entity.Id)
            .ExecuteDeleteAsync();
    }

    public async Task Update(TeachersDepartments entity) =>
        await _context
            .Departments.Where(d => d.Id == entity.Id)
            .ExecuteUpdateAsync(d => d.SetProperty(d => d.Name.Name, entity.Name.Name));
}
