using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.DataAccess.Repositories;

public class StudentAssignmentsRepository : IStudentAssignmentsRepository
{
    private readonly DatabaseContext _context = new();

    public async Task<StudentAssignment?> ReceiveAssignment(
        Guid id,
        CancellationToken ct = default
    ) =>
        await _context
            .StudentAssignments.Include(a => a.Assignment)
            .ThenInclude(d => d.Discipline)
            .Include(a => a.Student)
            .ThenInclude(s => s.AttachedGroup)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken: ct);

    public async Task UpdateAssignmentValue(
        StudentAssignment assignment,
        CancellationToken ct = default
    ) =>
        await _context
            .StudentAssignments.Where(a => a.Id == assignment.Id)
            .ExecuteUpdateAsync(
                a => a.SetProperty(a => a.Value.Value, assignment.Value.Value),
                cancellationToken: ct
            );
}
