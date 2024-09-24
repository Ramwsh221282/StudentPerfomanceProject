using Microsoft.EntityFrameworkCore;

using StudentPerfomance.DataAccess.Repositories.Sortings;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.StudentGroups;

public sealed class StudentGroupsRepository : IRepository<StudentGroup>
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<IReadOnlyCollection<StudentGroup>> GetAll() =>
		await _context.Groups
		.Include(g => g.Students)
		.OrderByDescending(g => g.Name.Name)
		.AsNoTracking()
		.ToListAsync();

	public async Task<int> Count() => await _context.Groups.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.AnyAsync(expression.Build());

	public async Task Create(StudentGroup entity)
	{
		await _context.Groups.AddAsync(entity);
		await Commit();
	}

	public async Task Remove(StudentGroup entity)
	{
		await _context.Students.Where(s => s.Group.Name.Name == entity.Name.Name)
		.ExecuteDeleteAsync();
		await _context.SemesterPlans.Where(s => s.LinkedSemester.Group.Id == entity.Id)
		.ExecuteDeleteAsync();
		await _context.Semesters.Where(s => s.Group.Id == entity.Id)
		.ExecuteDeleteAsync();
		await _context.Groups.Where(g => g.Name.Name == entity.Name.Name)
		.ExecuteDeleteAsync();
		await Commit();
	}

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> GetPaged(int page, int pageSize)
	{
		List<StudentGroup> groups = await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
		groups.Sort(new AlphabetSorting());
		return groups;
	}

	public async Task<StudentGroup?> GetByParameter(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<StudentGroup>> GetFiltered(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.OrderByDescending(g => g.Name.Name)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> GetFilteredAndPaged(IRepositoryExpression<StudentGroup> expression, int page, int pageSize)
	{
		List<StudentGroup> groups = await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.OrderByDescending(g => g.Name.Name)
		.Where(expression.Build())
		.Skip((1 - page) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
		groups.Sort(new AlphabetSorting());
		return groups;
	}

	public async Task<int> CountWithExpression(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups.CountAsync(expression.Build());
}
