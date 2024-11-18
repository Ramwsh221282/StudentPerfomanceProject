using Microsoft.EntityFrameworkCore;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Tools;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Repositories;

public sealed class ControlWeekRepository : IControlWeekReportRepository
{
    private readonly StatisticsDatabaseContext _context = new StatisticsDatabaseContext();

    public async Task<Result<AssignmentSessionView>> Insert(AssignmentSessionView view)
    {
        if (
            await _context.ControlWeekReports.AnyAsync(w =>
                (
                    w.CreationDate == DateTime.Parse(view.StartDate)
                    && w.CompletionDate == DateTime.Parse(view.StartDate)
                )
                || w.Id == view.Id
            )
        )
            return new Error("Отчёт по контрольной неделе уже существует");

        ControlWeekReportEntity entity = ControlWeekReportEntity.CreateReport(view);
        await _context.ControlWeekReports.AddAsync(entity);
        await _context.SaveChangesAsync();
        return view;
    }

    public async Task<IReadOnlyList<ControlWeekReportEntity>> GetPaged(int page, int pageSize) =>
        await _context
            .ControlWeekReports.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.GroupParts)
            .ThenInclude(g => g.Parts)
            .ThenInclude(d => d.Parts)
            .Include(r => r.CourseParts)
            .Include(r => r.DirectionCodeReport)
            .Include(r => r.DirectionTypeReport)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IReadOnlyList<ControlWeekReportEntity>> GetPagedFilteredByPeriod(
        int page,
        int pageSize,
        DateTime startDate,
        DateTime endDate
    ) =>
        await _context
            .ControlWeekReports.Where(r =>
                r.CompletionDate >= startDate && r.CompletionDate <= endDate
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(r => r.GroupParts)
            .ThenInclude(g => g.Parts)
            .ThenInclude(d => d.Parts)
            .Include(r => r.CourseParts)
            .Include(r => r.DirectionCodeReport)
            .Include(r => r.DirectionTypeReport)
            .AsNoTracking()
            .ToListAsync();
}
