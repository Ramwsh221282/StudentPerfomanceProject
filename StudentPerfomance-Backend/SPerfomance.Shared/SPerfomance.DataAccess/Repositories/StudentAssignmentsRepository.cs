using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.DataAccess.Repositories;

public class StudentAssignmentsRepository : IStudentAssignmentsRepository
{
	private readonly DatabaseContext _context = new DatabaseContext();

	public async Task<StudentAssignment?> ReceiveAssignment(Guid id) =>
		await _context.StudentAssignments.FirstOrDefaultAsync(a => a.Id == id);

	public async Task UpdateAssignmentValue(StudentAssignment assignment) =>
		await _context.StudentAssignments.Where(a => a.Id == assignment.Id)
		.ExecuteUpdateAsync(a =>
		a.SetProperty(a => a.Value.Value, assignment.Value.Value));
}
