using Microsoft.EntityFrameworkCore;
using SPerfomance.DataAccess.Module.Shared;

namespace SPerfomance.Application.EducationPlans.Module.Repository;

internal sealed class SemesterQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() => await _context.Semesters.CountAsync();
	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count + 1;
	}
}
