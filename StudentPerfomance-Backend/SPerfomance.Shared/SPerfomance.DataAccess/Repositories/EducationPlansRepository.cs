using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class EducationPlansRepository : IEducationPlansRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public async Task Insert(EducationPlan entity)
    {
        entity.SetNumber(await GenerateEntityNumber());

        int current = await GenerateSemestersEntityNumber();
        foreach (var semester in entity.Semesters)
        {
            semester.SetNumber(current);
            current++;
        }

        _context.EducationDirections.Attach(entity.Direction);
        await _context.EducationPlans.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<int> Count() => await _context.EducationPlans.CountAsync();

    public async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.EducationPlans.Select(s => s.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    private async Task<int> GenerateSemestersEntityNumber()
    {
        int[] numbers = await _context.Semesters.Select(s => s.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public async Task<IReadOnlyCollection<EducationPlan>> GetAll() =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task Remove(EducationPlan entity)
    {
        await _context
            .EducationPlans.Include(p => p.Groups)
            .ThenInclude(p => p.Students)
            .Include(p => p.Semesters)
            .Where(p => p.Id == entity.Id)
            .AsSplitQuery()
            .ExecuteDeleteAsync();
    }

    public async Task<bool> HasPlanWithYear(PlanYear year) =>
        await _context.EducationPlans.AnyAsync(p => p.Year.Year == year.Year);

    public async Task<bool> HasPlanWithYear(int year) =>
        await _context.EducationPlans.AnyAsync(p => p.Year.Year == year);

    public async Task Update(EducationPlan entity) =>
        await _context
            .EducationPlans.Where(p => p.Id == entity.Id)
            .ExecuteUpdateAsync(p => p.SetProperty(p => p.Year.Year, entity.Year.Year));

    public async Task<bool> HasPlan(EducationDirection direction, int year) =>
        await _context.EducationPlans.AnyAsync(p =>
            p.Direction.Id == direction.Id && p.Year.Year == year
        );

    public async Task<IReadOnlyCollection<EducationPlan>> GetFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year
    ) =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .Where(p =>
                !string.IsNullOrWhiteSpace(directionName)
                    && p.Direction.Name.Name.Contains(directionName)
                || !string.IsNullOrWhiteSpace(directionCode)
                    && p.Direction.Code.Code.Contains(directionCode)
                || !string.IsNullOrWhiteSpace(directionType)
                    && p.Direction.Type.Type.Contains(directionType)
                || year == p.Year.Year
            )
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task<IReadOnlyCollection<EducationPlan>> GetPagedFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year,
        int page,
        int pageSize
    ) =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .OrderBy(p => p.EntityNumber)
            .Where(p =>
                !string.IsNullOrWhiteSpace(directionName)
                    && p.Direction.Name.Name.Contains(directionName)
                || !string.IsNullOrWhiteSpace(directionCode)
                    && p.Direction.Code.Code.Contains(directionCode)
                || !string.IsNullOrWhiteSpace(directionType)
                    && p.Direction.Type.Type.Contains(directionType)
                || year == p.Year.Year
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task<IReadOnlyCollection<EducationPlan>> GetPaged(int page, int pageSize) =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .OrderBy(p => p.EntityNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();
}
