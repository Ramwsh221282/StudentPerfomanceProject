using Microsoft.EntityFrameworkCore;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Tools;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Repositories;

public sealed class ControlWeekRepository : IControlWeekReportRepository
{
    private readonly StatisticsDatabaseContext _context = new();

    public async Task<Result<AssignmentSessionView>> Insert(
        AssignmentSessionView view,
        CancellationToken ct = default
    )
    {
        if (
            await _context.ControlWeekReports.AnyAsync(
                w =>
                    (
                        w.CreationDate == DateTime.Parse(view.StartDate)
                        && w.CompletionDate == DateTime.Parse(view.StartDate)
                    )
                    || w.Id == view.Id,
                cancellationToken: ct
            )
        )
            return new Error("Отчёт по контрольной неделе уже существует");

        var entity = ControlWeekReportEntity.CreateReport(view);
        await _context.ControlWeekReports.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
        return view;
    }

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.ControlWeekReports.CountAsync(cancellationToken: ct);

    public async Task<bool> Remove(string id, CancellationToken ct = default)
    {
        Guid comparisonId = Guid.Parse(id);
        if (
            !await _context.ControlWeekReports.AnyAsync(
                r => r.Id == comparisonId,
                cancellationToken: ct
            )
        )
            return false;
        await _context
            .ControlWeekReports.Where(r => r.Id == comparisonId)
            .ExecuteDeleteAsync(cancellationToken: ct);
        return true;
    }

    public async Task<IReadOnlyList<ControlWeekReportEntity>> GetPaged(
        int page,
        int pageSize,
        CancellationToken ct = default
    ) =>
        await _context
            .ControlWeekReports.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.GroupParts.OrderBy(g => g.GroupName))
            .ThenInclude(g => g.Parts)
            .ThenInclude(d => d.Parts.OrderBy(s => s.StudentSurname))
            .Include(r => r.CourseParts)
            .Include(r => r.DirectionCodeReport)
            .Include(r => r.DirectionTypeReport)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);

    public async Task<IReadOnlyList<ControlWeekReportEntity>> GetPagedFilteredByPeriod(
        int page,
        int pageSize,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct = default
    )
    {
        var query = _context.ControlWeekReports.AsQueryable();
        if (startDate != null)
            query = query.Where(r => r.CreationDate >= startDate);

        if (endDate != null)
            query = query.Where(r => r.CompletionDate <= endDate);

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.GroupParts.OrderBy(g => g.GroupName))
            .ThenInclude(g => g.Parts)
            .ThenInclude(d => d.Parts.OrderBy(s => s.StudentSurname))
            .Include(r => r.CourseParts)
            .Include(r => r.DirectionCodeReport)
            .Include(r => r.DirectionTypeReport)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<ControlWeekReportEntity?> GetById(Guid id, CancellationToken ct = default) =>
        await _context
            .ControlWeekReports.Include(r => r.GroupParts.OrderBy(g => g.GroupName))
            .ThenInclude(g => g.Parts)
            .ThenInclude(d => d.Parts.OrderBy(s => s.StudentSurname))
            .Include(r => r.CourseParts)
            .Include(r => r.DirectionCodeReport)
            .Include(r => r.DirectionTypeReport)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken: ct);

    public async Task<ControlWeekReportEntity?> GetDirectionCodeTypeCourseReportsById(
        Guid controlWeekReportId,
        CancellationToken ct = default
    ) =>
        await _context
            .ControlWeekReports.Include(r => r.GroupParts.OrderBy(g => g.GroupName))
            .Include(r => r.CourseParts.OrderBy(c => c.Course))
            .Include(r => r.DirectionCodeReport.OrderBy(c => c.DirectionCode))
            .Include(r => r.DirectionTypeReport.OrderBy(c => c.DirectionType))
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == controlWeekReportId, cancellationToken: ct);
}
