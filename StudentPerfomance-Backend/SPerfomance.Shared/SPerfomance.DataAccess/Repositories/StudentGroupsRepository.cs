using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class StudentGroupsRepository : IStudentGroupsRepository
{
    private readonly DatabaseContext _context = new();

    public async Task AttachEducationPlanId(StudentGroup group, CancellationToken ct = default)
    {
        const string sql =
            "UPDATE GROUPS SET EducationPlanId = @EducationPlanId, ActiveGroupSemesterId = @SemesterId WHERE Id = @Id";
        SqliteParameter[] parameters =
        [
            new("@EducationPlanId", group.EducationPlan!.Id),
            new("@Id", group.Id),
            new("@SemesterId", group.ActiveGroupSemester!.Id),
        ];
        await _context.Database.ExecuteSqlRawAsync(sql, parameters, ct);
    }

    public async Task DeattachEducationPlanId(StudentGroup group, CancellationToken ct = default)
    {
        const string sql =
            "UPDATE GROUPS SET EducationPlanId = NULL, ActiveGroupSemesterId = NULL WHERE Id = @Id";
        var parameter = new SqliteParameter("@Id", group.Id);
        await _context.Database.ExecuteSqlRawAsync(sql, parameter);
    }

    public async Task SetNextSemester(StudentGroup group, CancellationToken ct = default)
    {
        const string sql = """
            UPDATE GROUPS SET ActiveGroupSemesterId = @SemesterId WHERE Id = @Id
            """;
        SqliteParameter[] parameters =
        [
            new("@Id", group.Id),
            new("@SemesterId", group.ActiveGroupSemester!.Id),
        ];
        await _context.Database.ExecuteSqlRawAsync(sql, parameters, ct);
    }

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.Groups.CountAsync(ct);

    public async Task<IReadOnlyCollection<StudentGroup>> Filter(
        string? name,
        CancellationToken ct = default
    ) =>
        await _context
            .Groups.Where(g => !string.IsNullOrWhiteSpace(name) && g.Name.Name.Contains(name))
            .Include(g => g.ActiveGroupSemester)
            .ThenInclude(a => a!.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(g => g.Students)
            .Include(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<StudentGroup>> FilterPaged(
        string? name,
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .Groups.Where(g => !string.IsNullOrWhiteSpace(name) && g.Name.Name.Contains(name))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(g => g.Name.Name)
            .Include(g => g.ActiveGroupSemester)
            .ThenInclude(a => a!.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(g => g.Students)
            .Include(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Groups.Select(g => g.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task<StudentGroup?> Get(string name, CancellationToken ct = default) =>
        await _context
            .Groups.Include(g => g.ActiveGroupSemester)
            .ThenInclude(a => a!.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(g => g.Students)
            .Include(g => g.EducationPlan)
            .ThenInclude(p => p!.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(g => g.Name.Name == name, cancellationToken: ct);

    public async Task<StudentGroup?> GetById(Guid id, CancellationToken ct = default)
    {
        StudentGroup? group = await _context
            .Groups.Include(g => g.ActiveGroupSemester)
            .ThenInclude(a => a!.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(g => g.Students)
            .Include(g => g.EducationPlan)
            .ThenInclude(p => p!.Semesters)
            .ThenInclude(s => s.Disciplines)
            .ThenInclude(d => d.Teacher)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(g => g.Id == id, cancellationToken: ct);
        return group;
    }

    public async Task<IReadOnlyCollection<StudentGroup>> GetAll(CancellationToken ct = default) =>
        await _context
            .Groups.Include(g => g.ActiveGroupSemester)
            .ThenInclude(a => a!.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(g => g.Students)
            .Include(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyCollection<StudentGroup>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .Groups.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(g => g.Name.Name)
            .Include(g => g.ActiveGroupSemester)
            .ThenInclude(a => a!.Disciplines)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(g => g.Students)
            .Include(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task<bool> HasWithName(string name, CancellationToken ct = default) =>
        await _context.Groups.AnyAsync(g => g.Name.Name == name, cancellationToken: ct);

    public async Task Insert(StudentGroup entity, CancellationToken ct = default)
    {
        entity.SetNumber(await GenerateEntityNumber(ct));
        await _context.Groups.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Remove(StudentGroup entity, CancellationToken ct = default) =>
        await _context
            .Groups.Where(g => g.Id == entity.Id)
            .ExecuteDeleteAsync(cancellationToken: ct);

    public async Task Update(StudentGroup entity, CancellationToken ct = default) =>
        await _context
            .Groups.Where(g => g.Id == entity.Id)
            .ExecuteUpdateAsync(
                g => g.SetProperty(g => g.Name.Name, entity.Name.Name),
                cancellationToken: ct
            );

    public async Task UpdateMerge(
        StudentGroup initial,
        StudentGroup target,
        CancellationToken ct = default
    )
    {
        const string sql =
            "UPDATE Students SET AttachedGroupId = @AttachedGroupId WHERE AttachedGroupId = @CurrentGroupId";
        SqliteParameter[] parameters =
        [
            new SqliteParameter("@AttachedGroupId", initial.Id),
            new SqliteParameter("@CurrentGroupId", target.Id),
        ];
        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }
}
