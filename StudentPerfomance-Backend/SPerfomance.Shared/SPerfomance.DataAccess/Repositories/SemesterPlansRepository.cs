using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class SemesterPlansRepository : ISemesterPlansRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public async Task DeattachTeacherId(SemesterPlan entity)
    {
        string sql = """
            UPDATE SemesterPlans
            SET TeacherId = NULL
            WHERE Id = @SemesterPlanId
            """;
        SqliteParameter parameter = new SqliteParameter("@SemesterPlanId", entity.Id);
        await _context.Database.ExecuteSqlRawAsync(sql, parameter);
    }

    public async Task AttachTeacherId(SemesterPlan entity)
    {
        string sql = """
            UPDATE SemesterPlans
            SET TeacherId = @TeacherId
            WHERE Id = @SemesterPlanId
            """;
        SqliteParameter[] parameters = new SqliteParameter[]
        {
            new SqliteParameter("@TeacherId", entity.Teacher!.Id),
            new SqliteParameter("@SemesterPlanId", entity.Id),
        };
        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }

    public async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.SemesterPlans.Select(s => s.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public async Task Insert(SemesterPlan entity)
    {
        entity.SetNumber(await GenerateEntityNumber());
        string sql = """
            INSERT INTO SemesterPlans
            (Id, Discipline_Name, SemesterId, EntityNumber)
            VALUES (@Id, @Discipline, @SemesterId, @EntityNumber);
            """;
        SqliteParameter[] parameters = new SqliteParameter[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@Discipline", entity.Discipline.Name),
            new SqliteParameter("@SemesterId", entity.Semester.Id),
            new SqliteParameter("@EntityNumber", entity.EntityNumber),
        };
        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }

    public async Task Remove(SemesterPlan entity) =>
        await _context.SemesterPlans.Where(s => s.Id == entity.Id).ExecuteDeleteAsync();

    public async Task Update(SemesterPlan entity) =>
        await _context
            .SemesterPlans.Where(s => s.Id == entity.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(s => s.Discipline.Name, entity.Discipline.Name));
}
