using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class EducationPlansRepository : IEducationPlansRepository
{
    private readonly DatabaseContext _context = new();

    public async Task Insert(EducationPlan entity, CancellationToken ct = default)
    {
        entity.SetNumber(await GenerateEntityNumber(ct));
        const string planSql = """
            INSERT INTO EducationPlans
            (Id, DirectionId, Year_Year, EntityNumber)
            VALUES
            (@Id, @DirectionId, @Year_Year, @EntityNumber)
            """;

        const string semesterSql = """
            INSERT INTO Semesters
            (Id, PlanId, Number_Number, EntityNumber)
            VALUES
            (@Id, @PlanId, @Number_Number, @EntityNumber)
            """;

        var planParameters = new[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@DirectionId", entity.Direction.Id),
            new SqliteParameter("@Year_Year", entity.Year.Year),
            new SqliteParameter("@EntityNumber", entity.EntityNumber),
        };
        await _context.Database.ExecuteSqlRawAsync(planSql, planParameters, ct);

        var current = await GenerateSemestersEntityNumber(ct);
        foreach (var semester in entity.Semesters)
        {
            semester.SetNumber(current);
            current++;
            var semesterParameters = new[]
            {
                new SqliteParameter("@Id", semester.Id),
                new SqliteParameter("@PlanId", semester.Plan.Id),
                new SqliteParameter("@Number_Number", semester.Number.Number),
                new SqliteParameter("@EntityNumber", semester.EntityNumber),
            };
            await _context.Database.ExecuteSqlRawAsync(semesterSql, semesterParameters, ct);
        }
    }

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.EducationPlans.CountAsync(cancellationToken: ct);

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .EducationPlans.Select(s => s.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    private async Task<int> GenerateSemestersEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Semesters.Select(s => s.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task<IReadOnlyCollection<EducationPlan>> GetAll(CancellationToken ct = default) =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .ThenInclude(t => t!.Department)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task Remove(EducationPlan entity, CancellationToken ct = default)
    {
        await _context
            .EducationPlans.Include(p => p.Groups)
            .ThenInclude(p => p.Students)
            .Include(p => p.Semesters)
            .Where(p => p.Id == entity.Id)
            .AsSplitQuery()
            .ExecuteDeleteAsync(cancellationToken: ct);
    }

    public async Task<bool> HasPlanWithYear(PlanYear year, CancellationToken ct = default) =>
        await _context.EducationPlans.AnyAsync(
            p => p.Year.Year == year.Year,
            cancellationToken: ct
        );

    public async Task<bool> HasPlanWithYear(int year, CancellationToken ct = default) =>
        await _context.EducationPlans.AnyAsync(p => p.Year.Year == year, cancellationToken: ct);

    public async Task Update(EducationPlan entity, CancellationToken ct = default) =>
        await _context
            .EducationPlans.Where(p => p.Id == entity.Id)
            .ExecuteUpdateAsync(
                p => p.SetProperty(p => p.Year.Year, entity.Year.Year),
                cancellationToken: ct
            );

    public async Task<bool> HasPlan(
        EducationDirection direction,
        int year,
        CancellationToken ct = default
    ) =>
        await _context.EducationPlans.AnyAsync(
            p => p.Direction.Id == direction.Id && p.Year.Year == year,
            cancellationToken: ct
        );

    public async Task<IReadOnlyCollection<EducationPlan>> GetFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year,
        CancellationToken ct = default
    ) =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .ThenInclude(t => t!.Department)
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
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<EducationPlan>> GetPagedFiltered(
        string? directionName,
        string? directionCode,
        string? directionType,
        int? year,
        int page,
        int pageSize,
        CancellationToken ct = default
    )
    {
        var plansQuery = _context.EducationPlans.AsQueryable();
        if (year != null && year.Value != 0)
            plansQuery = plansQuery.Where(p => p.Year.Year == year.Value);
        if (!string.IsNullOrWhiteSpace(directionName))
            plansQuery = plansQuery.Where(p => p.Direction.Name.Name == directionName);
        if (!string.IsNullOrWhiteSpace(directionCode))
            plansQuery = plansQuery.Where(p => p.Direction.Code.Code == directionCode);
        if (!string.IsNullOrWhiteSpace(directionType))
            plansQuery = plansQuery.Where(p => p.Direction.Type.Type == directionType);
        plansQuery = plansQuery.OrderBy(p => p.EntityNumber);
        plansQuery = plansQuery.Skip((page - 1) * pageSize);
        plansQuery = plansQuery.Take(pageSize);
        plansQuery = plansQuery
            .Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department);
        return await plansQuery.AsNoTracking().AsSplitQuery().ToListAsync(ct);
    }

    public async Task<IReadOnlyCollection<EducationPlan>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .EducationPlans.Include(p => p.Direction)
            .Include(p => p.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(c => c.Teacher)
            .ThenInclude(t => t!.Department)
            .OrderBy(p => p.EntityNumber)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);
}
