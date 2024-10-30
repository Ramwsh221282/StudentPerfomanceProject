using Microsoft.EntityFrameworkCore;

using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class EducationDirectionRepository : IEducationDirectionRepository
{
	private readonly DatabaseContext _context = new DatabaseContext();

	public async Task Insert(EducationDirection direction)
	{
		direction.SetNumber(await GenerateEntityNumber());
		await _context.EducationDirections.AddAsync(direction);
		await _context.SaveChangesAsync();
	}

	public async Task<int> Count() => await _context.EducationDirections.CountAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.EducationDirections.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}

	public async Task<IReadOnlyCollection<EducationDirection>> GetAll() =>
		await _context.EducationDirections
		.OrderByDescending(d => d.EntityNumber)
		.Include(d => d.Plans)
		.ThenInclude(p => p.Semesters)
		.ThenInclude(s => s.Disciplines)
		.ThenInclude(d => d.Teacher)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<IReadOnlyCollection<EducationDirection>> GetPaged(int page, int pageSize) =>
		await _context.EducationDirections
		.OrderBy(d => d.EntityNumber)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.Include(d => d.Plans)
		.ThenInclude(p => p.Semesters)
		.ThenInclude(s => s.Disciplines)
		.ThenInclude(d => d.Teacher)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task Remove(EducationDirection direction)
	{
		await _context.EducationDirections.Where(d => d.Id == direction.Id)
		.ExecuteDeleteAsync();
	}

	public async Task Update(EducationDirection direction)
	{
		await _context.EducationDirections
		.Where(d => d.Id == direction.Id)
		.ExecuteUpdateAsync(
			d => d.SetProperty(d => d.Name.Name, direction.Name.Name)
			.SetProperty(d => d.Type.Type, direction.Type.Type)
			.SetProperty(d => d.Code.Code, direction.Code.Code));
	}

	public async Task<EducationDirection?> Get(string code, string name, string type) =>
		await _context.EducationDirections
		.Include(d => d.Plans)
		.ThenInclude(p => p.Semesters)
		.ThenInclude(s => s.Disciplines)
		.ThenInclude(d => d.Teacher)
		.ThenInclude(t => t!.Department)
		.AsNoTracking()
		.AsSplitQuery()
		.FirstOrDefaultAsync(d =>
		d.Code.Code == code &&
		d.Name.Name == name &&
		d.Type.Type == type);

	public async Task<EducationDirection?> GetByCode(string code) =>
		await _context.EducationDirections
		.Include(d => d.Plans)
		.ThenInclude(p => p.Semesters)
		.ThenInclude(s => s.Disciplines)
		.ThenInclude(d => d.Teacher)
		.AsNoTracking()
		.AsSplitQuery()
		.FirstOrDefaultAsync(d => d.Code.Code == code);

	public async Task<IReadOnlyCollection<EducationDirection>> GetFiltered(string? code, string? name, string? type) =>
		await _context.EducationDirections
			.OrderBy(d => d.EntityNumber)
			.Where(
				d =>
				!string.IsNullOrWhiteSpace(code) && d.Code.Code.Contains(code) ||
				!string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name) ||
				!string.IsNullOrWhiteSpace(type) && d.Type.Type == type
			)
			.Include(d => d.Plans)
			.ThenInclude(p => p.Semesters)
			.ThenInclude(s => s.Disciplines)
			.ThenInclude(d => d.Teacher)
			.AsNoTracking()
			.AsSplitQuery()
			.ToListAsync();

	public async Task<IReadOnlyCollection<EducationDirection>> GetPagedFiltered(
		string? code,
		string? name,
		string? type,
		int page,
		int pageSize) =>
			await _context.EducationDirections
			.OrderBy(d => d.EntityNumber)
			.Where(
				d =>
				!string.IsNullOrWhiteSpace(code) && d.Code.Code.Contains(code) ||
				!string.IsNullOrWhiteSpace(name) && d.Name.Name.Contains(name) ||
				!string.IsNullOrWhiteSpace(type) && d.Type.Type == type
			)
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.Include(d => d.Plans)
			.ThenInclude(p => p.Semesters)
			.ThenInclude(s => s.Disciplines)
			.ThenInclude(d => d.Teacher)
			.AsNoTracking()
			.AsSplitQuery()
			.ToListAsync();

	public async Task<bool> Has(string code, string name, string type) =>
		await _context.EducationDirections.AnyAsync(
			d => d.Code.Code == code &&
			 d.Name.Name == name &&
			 d.Type.Type == type);

	public async Task<bool> HasWithCode(string code) =>
		await _context.EducationDirections.AnyAsync(d => d.Code.Code == code);
}
