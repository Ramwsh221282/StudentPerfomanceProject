using Microsoft.EntityFrameworkCore;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.EducationPlans.Module.Repository;

internal sealed class SemesterQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task<int> Count() => await _context.Semesters.CountAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Semesters.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
