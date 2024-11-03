using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class StudentGroupsRepository : IStudentGroupsRepository
{
	private readonly DatabaseContext _context = new DatabaseContext();

	public async Task AttachEducationPlanId(StudentGroup group)
	{
		string sql = "UPDATE GROUPS SET EducationPlanId = @EducationPlanId, ActiveGroupSemesterId = @SemesterId WHERE Id = @Id";
		SqliteParameter[] parameters =
		[
			new("@EducationPlanId", group.EducationPlan!.Id),
			new("@Id", group.Id),
			new("@SemesterId", group.ActiveGroupSemester!.Id)
			];
		await _context.Database.ExecuteSqlRawAsync(sql, parameters);
	}

	public async Task DeattachEducationPlanId(StudentGroup group)
	{
		string sql = "UPDATE GROUPS SET EducationPlanId = NULL, ActiveGroupSemesterId = NULL WHERE Id = @Id";
		SqliteParameter parameter = new SqliteParameter("@Id", group.Id);
		await _context.Database.ExecuteSqlRawAsync(sql, parameter);
	}

	public async Task<int> Count() => await _context.Groups.CountAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> Filter(string? name) =>
		await _context.Groups
		.Where(g => !string.IsNullOrWhiteSpace(name) && g.Name.Name.Contains(name))
		.Include(g => g.ActiveGroupSemester)
		.ThenInclude(a => a!.Disciplines)
		.ThenInclude(d => d.Teacher)
		.ThenInclude(t => t!.Department)
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p!.Direction)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> FilterPaged(string? name, int page, int pageSize) =>
		await _context.Groups
		.Where(g => !string.IsNullOrWhiteSpace(name) && g.Name.Name.Contains(name))
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.Include(g => g.ActiveGroupSemester)
		.ThenInclude(a => a!.Disciplines)
		.ThenInclude(d => d.Teacher)
		.ThenInclude(t => t!.Department)
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p!.Direction)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Groups.Select(g => g.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}

	public async Task<StudentGroup?> Get(string name) =>
		await _context.Groups
		.Include(g => g.ActiveGroupSemester)
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
		.FirstOrDefaultAsync(g => g.Name.Name == name);


	public async Task<IReadOnlyCollection<StudentGroup>> GetAll() =>
		await _context.Groups
		.Include(g => g.ActiveGroupSemester)
		.ThenInclude(a => a!.Disciplines)
		.ThenInclude(d => d.Teacher)
		.ThenInclude(t => t!.Department)
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p!.Direction)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> GetPaged(int page, int pageSize) =>
		await _context.Groups
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.Include(g => g.ActiveGroupSemester)
		.ThenInclude(a => a!.Disciplines)
		.ThenInclude(d => d.Teacher)
		.ThenInclude(t => t!.Department)
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p!.Direction)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<bool> HasWithName(string name) => await _context.Groups.AnyAsync(g => g.Name.Name == name);

	public async Task Insert(StudentGroup entity)
	{
		entity.SetNumber(await GenerateEntityNumber());
		await _context.Groups.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async Task Remove(StudentGroup entity) =>
		await _context.Groups
		.Where(g => g.Id == entity.Id)
		.ExecuteDeleteAsync();

	public async Task Update(StudentGroup entity) =>
		await _context.Groups.Where(g => g.Id == entity.Id)
		.ExecuteUpdateAsync(g =>
		g.SetProperty(g => g.Name.Name, entity.Name.Name));

	public async Task UpdateMerge(StudentGroup target, StudentGroup merged)
	{
		string sql = "UPDATE Students SET AttachedGroupId = @AttachedGroupId WHERE Id = @CurrentGroupId";
		SqliteParameter[] parameters = [
			new SqliteParameter("@AttachedGroupId", target.Id),
			new SqliteParameter("@CurrentGroupId", merged.Id)
		];
		await _context.Database.ExecuteSqlRawAsync(sql, parameters);
	}
}
