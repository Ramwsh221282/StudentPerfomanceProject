using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class SemesterPlansRepository : ISemesterPlansRepository
{
    private readonly DatabaseContext _context = new();

    public async Task DeattachTeacherId(SemesterPlan entity, CancellationToken ct = default)
    {
        const string sql = """
            UPDATE SemesterPlans
            SET TeacherId = NULL
            WHERE Id = @SemesterPlanId
            """;
        var parameter = new SqliteParameter("@SemesterPlanId", entity.Id);
        await _context.Database.ExecuteSqlRawAsync(sql, parameter);
    }

    public async Task AttachTeacherId(SemesterPlan entity, CancellationToken ct = default)
    {
        const string sql = """
            UPDATE SemesterPlans
            SET TeacherId = @TeacherId
            WHERE Id = @SemesterPlanId
            """;
        var parameters = new[]
        {
            new SqliteParameter("@TeacherId", entity.Teacher!.Id),
            new SqliteParameter("@SemesterPlanId", entity.Id),
        };
        await _context.Database.ExecuteSqlRawAsync(sql, parameters, ct);
    }

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .SemesterPlans.Select(s => s.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task Insert(SemesterPlan entity, CancellationToken ct = default)
    {
        entity.SetNumber(await GenerateEntityNumber(ct));
        const string sql = """
            INSERT INTO SemesterPlans
            (Id, Discipline_Name, SemesterId, EntityNumber)
            VALUES (@Id, @Discipline, @SemesterId, @EntityNumber);
            """;
        var parameters = new[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@Discipline", entity.Discipline.Name),
            new SqliteParameter("@SemesterId", entity.Semester.Id),
            new SqliteParameter("@EntityNumber", entity.EntityNumber),
        };
        await _context.Database.ExecuteSqlRawAsync(sql, parameters, ct);
    }

    public async Task Remove(SemesterPlan entity, CancellationToken ct = default) =>
        await _context
            .SemesterPlans.Where(s => s.Id == entity.Id)
            .ExecuteDeleteAsync(cancellationToken: ct);

    public async Task Update(SemesterPlan entity, CancellationToken ct = default) =>
        await _context
            .SemesterPlans.Where(s => s.Id == entity.Id)
            .ExecuteUpdateAsync(
                s => s.SetProperty(s => s.Discipline.Name, entity.Discipline.Name),
                cancellationToken: ct
            );
}
