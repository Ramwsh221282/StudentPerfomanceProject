using Microsoft.EntityFrameworkCore;
using SPerfomance.Statistics.DataAccess.Configurations;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess;

internal sealed class StatisticsDatabaseContext : DbContext
{
    public DbSet<ControlWeekReportEntity> ControlWeekReports { get; set; } = null!;
    public DbSet<GroupStatisticsReportEntity> GroupStatisticsReports { get; set; } = null!;
    public DbSet<DisciplinesStatisticsReportEntity> DisciplinesStatisticsReports { get; set; } =
        null!;
    public DbSet<StudentStatisticsReportEntity> StudentStatisticsReports { get; set; } = null!;
    public DbSet<CourseStatisticsReportEntity> CourseStatisticsReportEntities { get; set; } = null!;
    public DbSet<DirectionCodeStatisticsReportEntity> DirectionCodeStatisticsReportEntities { get; set; } =
        null!;
    public DbSet<DirectionTypeStatisticsReportEntity> DirectionTypeStatisticsReportEntities { get; set; } =
        null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=StatisticsDatabase.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
            .ApplyConfiguration(new ControlWeekReportEntityConfiguration())
            .ApplyConfiguration(new GroupStatisticsReportConfiguration())
            .ApplyConfiguration(new DisciplineStatisticsReportEntityConfiguration())
            .ApplyConfiguration(new StudentStatisticsReportEntityConfiguration())
            .ApplyConfiguration(new CourseStatisticsReportEntityConfiguration())
            .ApplyConfiguration(new DirectionCodeStatisticsReportConfiguration())
            .ApplyConfiguration(new DirectionTypeStatisticsReportEntityConfiguration());
}
