using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class StudentsRepository : IStudentsRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public async Task Insert(Student entity)
    {
        entity.SetNumber(await GenerateEntityNumber());
        string sql =
            "INSERT INTO Students (Id, Name_Name, Name_Surname, Name_Patronymic, State_State, Recordbook_Recordbook, AttachedGroupId, EntityNumber) VALUES (@Id, @Name, @Surname, @Patronymic, @State, @Recordbook, @GroupId, @Number)";
        SqliteParameter[] parameters =
        [
            new SqliteParameter("@Id", entity.Id),
            new SqliteParameter("@Name", entity.Name.Name),
            new SqliteParameter("@Surname", entity.Name.Surname),
            new SqliteParameter("@Patronymic", entity.Name.Patronymic),
            new SqliteParameter("@State", entity.State.State),
            new SqliteParameter("@Recordbook", entity.Recordbook.Recordbook),
            new SqliteParameter("@GroupId", entity.AttachedGroup.Id),
            new SqliteParameter("@Number", entity.EntityNumber),
        ];
        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }

    public async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.Students.Select(s => s.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public async Task Remove(Student entity) =>
        await _context.Students.Where(e => e.Id == entity.Id).ExecuteDeleteAsync();

    public async Task Update(Student entity) =>
        await _context
            .Students.Where(e => e.Id == entity.Id)
            .ExecuteUpdateAsync(e =>
                e.SetProperty(e => e.Name.Name, entity.Name.Name)
                    .SetProperty(e => e.Name.Name, entity.Name.Surname)
                    .SetProperty(e => e.State.State, entity.State.State)
                    .SetProperty(e => e.Name.Surname, entity.Name.Surname)
                    .SetProperty(e => e.Name.Patronymic, entity.Name.Patronymic)
                    .SetProperty(e => e.Recordbook.Recordbook, entity.Recordbook.Recordbook)
            );

    public async Task<bool> HasWithRecordbook(ulong recordbook) =>
        await _context.Students.AnyAsync(s => s.Recordbook.Recordbook == recordbook);

    public async Task UpdateWithGroupId(Student entity)
    {
        string sql = """
            UPDATE Students SET Name_Name = @Name, Name_Surname = @Surname, Name_Patronymic = @Patronymic, State_State = @State, Recordbook_Recordbook = @Recordbook, AttachedGroupId = @GroupId
            WHERE Id = @StudentId
            """;
        SqliteParameter[] parameters =
        [
            new SqliteParameter("@StudentId", entity.Id),
            new SqliteParameter("@Name", entity.Name.Name),
            new SqliteParameter("@Surname", entity.Name.Surname),
            new SqliteParameter("@Patronymic", entity.Name.Patronymic),
            new SqliteParameter("@State", entity.State.State),
            new SqliteParameter("@Recordbook", entity.Recordbook.Recordbook),
            new SqliteParameter("@GroupId", entity.AttachedGroup.Id),
        ];
        await _context.Database.ExecuteSqlRawAsync(sql, parameters);
    }
}
