using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Abstractions;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.Errors;
using SPerfomance.Domain.Tools;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess.Repositories;

public sealed class ControlWeekRepository : IControlWeekReportRepository
{
    private readonly StatisticsDatabaseContext _context = new StatisticsDatabaseContext();

    public async Task<Result<ControlWeekReport>> Insert(ControlWeekReport report)
    {
        if (
            await _context.ControlWeekReports.AnyAsync(w =>
                (
                    w.CreationDate == report.Period.CreationDate
                    && w.CompletionDate == report.Period.CompletionDate
                )
                || w.Id == report.Id
            )
        )
            return ControlWeekReportErrors.ReportDuplicateInDatabase();

        var entity = new ControlWeekReportEntity(report);
        entity.SetEntityNumber(await GenerateEntityNumber());
        await _context.ControlWeekReports.AddAsync(entity);
        await _context.SaveChangesAsync();
        return report;
    }

    public async Task<ControlWeekReportEntity?> GetById(Guid id) =>
        await _context
            .ControlWeekReports.Include(r => r.GroupParts)
            .ThenInclude(p => p.Parts)
            .ThenInclude(subp => subp.Parts)
            .Include(r => r.CourseReport)
            .ThenInclude(p => p.Parts)
            .Include(r => r.DepartmentReport)
            .ThenInclude(p => p.Parts)
            .ThenInclude(subp => subp.Parts)
            .Include(r => r.DirectionCodeReport)
            .ThenInclude(p => p.Parts)
            .Include(r => r.DirectionTypeReport)
            .ThenInclude(p => p.Parts)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(r => r.Id == id);

    private async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.ControlWeekReports.Select(r => r.RowNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }
}
