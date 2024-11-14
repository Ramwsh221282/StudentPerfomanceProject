using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class TeachersRepository : ITeachersRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public async Task Insert(Teacher entity)
    {
        entity.SetNumber(await GenerateEntityNumber());

        string sql = """
            INSERT INTO Teachers
            (Id, JobTitle_JobTitle, State_State, DepartmentId, Name_Name, Name_Patronymic, Name_Surname, EntityNumber)
            VALUES (@Id, @JobTitle, @State, @DepartmentId, @Name, @Patronymic, @Surname, @EntityNumber)
            """;

        SqliteParameter[] parameters = new[]
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
        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }

    public async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.Teachers.Select(t => t.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public async Task<Teacher?> GetByName(string name, string surname, string? patronymic) =>
        await _context
            .Teachers.Include(t => t.Department)
            .Include(t => t.Disciplines)
            .ThenInclude(d => d.Semester)
            .ThenInclude(s => s.Plan)
            .ThenInclude(p => p.Groups)
            .ThenInclude(g => g.Students)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(t =>
                t.Name.Name == name && t.Name.Surname == surname && t.Name.Patronymic == patronymic
            );

    public async Task Remove(Teacher entity) =>
        await _context.Teachers.Where(t => t.Id == entity.Id).ExecuteDeleteAsync();

    public async Task Update(Teacher entity) =>
        await _context
            .Teachers.Where(t => t.Id == entity.Id)
            .ExecuteUpdateAsync(t =>
                t.SetProperty(t => t.Name.Name, entity.Name.Name)
                    .SetProperty(t => t.Name.Surname, entity.Name.Surname)
                    .SetProperty(t => t.Name.Patronymic, entity.Name.Patronymic)
                    .SetProperty(t => t.JobTitle.JobTitle, entity.JobTitle.JobTitle)
                    .SetProperty(t => t.State.State, entity.State.State)
            );

    public async Task<Teacher?> Get(
        string name,
        string surname,
        string patronymic,
        string jobTitle,
        string workingState
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
            .FirstOrDefaultAsync(t =>
                t.Name.Name == name
                && t.Name.Surname == surname
                && t.Name.Patronymic == patronymic
                && t.JobTitle.JobTitle == jobTitle
                && t.State.State == workingState
            );
}
