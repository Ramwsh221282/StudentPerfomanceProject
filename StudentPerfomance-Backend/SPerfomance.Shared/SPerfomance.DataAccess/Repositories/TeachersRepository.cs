using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class TeachersRepository : ITeachersRepository
{
    private readonly DatabaseContext _context = new();

    public async Task Insert(Teacher entity, CancellationToken ct = default)
    {
        entity.SetNumber(await GenerateEntityNumber(ct));

        const string sql = """
            INSERT INTO Teachers
            (Id, JobTitle_JobTitle, State_State, DepartmentId, Name_Name, Name_Patronymic, Name_Surname, EntityNumber)
            VALUES (@Id, @JobTitle, @State, @DepartmentId, @Name, @Patronymic, @Surname, @EntityNumber)
            """;

        var parameters = new[]
        {
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@JobTitle", entity.JobTitle.JobTitle),
            new SqliteParameter("@State", entity.State.State),
            new SqliteParameter("@DepartmentId", entity.Department.Id),
            new SqliteParameter("@Name", entity.Name.Name),
            new SqliteParameter("@Patronymic", entity.Name.Patronymic),
            new SqliteParameter("@Surname", entity.Name.Surname),
            new SqliteParameter("@EntityNumber", entity.EntityNumber),
        };
        await _context.Database.ExecuteSqlRawAsync(sql, parameters, ct);
    }

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Teachers.Select(t => t.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task<Teacher?> GetByName(
        string name,
        string surname,
        string? patronymic,
        CancellationToken ct = default
    ) =>
        await _context
            .Teachers.Include(t => t.Department)
            .Include(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Groups)
            .ThenInclude(g => g.Students)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(
                t =>
                    t.Name.Name == name
                    && t.Name.Surname == surname
                    && t.Name.Patronymic == patronymic,
                cancellationToken: ct
            );

    public async Task Remove(Teacher entity, CancellationToken ct = default) =>
        await _context
            .Teachers.Where(t => t.Id == entity.Id)
            .ExecuteDeleteAsync(cancellationToken: ct);

    public async Task Update(Teacher entity, CancellationToken ct = default) =>
        await _context
            .Teachers.Where(t => t.Id == entity.Id)
            .ExecuteUpdateAsync(
                t =>
                    t.SetProperty(t => t.Name.Name, entity.Name.Name)
                        .SetProperty(t => t.Name.Surname, entity.Name.Surname)
                        .SetProperty(t => t.Name.Patronymic, entity.Name.Patronymic)
                        .SetProperty(t => t.JobTitle.JobTitle, entity.JobTitle.JobTitle)
                        .SetProperty(t => t.State.State, entity.State.State),
                cancellationToken: ct
            );

    public async Task<Teacher?> Get(
        string name,
        string surname,
        string patronymic,
        string jobTitle,
        string workingState,
        CancellationToken ct = default
    ) =>
        await _context
            .Teachers.Include(t => t.Department)
            .Include(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Groups)
            .ThenInclude(g => g.Students)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(
                t =>
                    t.Name.Name == name
                    && t.Name.Surname == surname
                    && t.Name.Patronymic == patronymic
                    && t.JobTitle.JobTitle == jobTitle
                    && t.State.State == workingState,
                cancellationToken: ct
            );
}
