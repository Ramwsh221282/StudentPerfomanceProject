using Microsoft.EntityFrameworkCore;
using SPerfomance.Statistics.DataAccess.Configurations;
using SPerfomance.Statistics.DataAccess.EntityModels;

namespace SPerfomance.Statistics.DataAccess;

internal sealed class StatisticsDatabaseContext : DbContext
{
    public DbSet<ControlWeekReportEntity> ControlWeekReports { get; set; } = null!;

    public DbSet<CourseReportEntity> CourseReports { get; set; } = null!;

    public DbSet<CourseReportEntityPart> CourseReportEntityParts { get; set; } = null!;

    public DbSet<DepartmentStatisticsReportEntity> DepartmentStatisticsReports { get; set; } =
        null!;

    public DbSet<DepartmentStatisticsReportPartEntity> DepartmentStatisticsReportParts { get; set; } =
        null!;

    public DbSet<DirectionCodeReportEntity> DirectionCodeReports { get; set; } = null!;

    public DbSet<DirectionCodeReportPartEntity> DirectionCodeReportParts { get; set; } = null!;

    public DbSet<DirectionTypeReportEntity> DirectionTypeReports { get; set; } = null!;

    public DbSet<DirectionTypeReportEntityPart> DirectionTypeReportParts { get; set; } = null!;

    public DbSet<GroupStatisticsReportEntity> GroupStatisticsReports { get; set; } = null!;

    public DbSet<StudentStatisticsPartEntity> StudentStatisticsReports { get; set; } = null!;

    public DbSet<StudentStatisticsOnDisciplinePartEntity> StudentStatisticsOnDisciplines { get; set; } =
        null!;

    public DbSet<TeacherStatisticsReportPartEntity> TeacherStatisticsReportParts { get; set; } =
        null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=StatisticsDatabase.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder
            .ApplyConfiguration(new ControlWeekReportEntityConfiguration())
            .ApplyConfiguration(new GroupStatisticsReportEntityConfiguration())
            .ApplyConfiguration(new StudentStatisticsPartEntityConfiguration())
            .ApplyConfiguration(new StudentStatisticsOnDisciplinePartEntityConfiguration())
            .ApplyConfiguration(new CourseReportEntityConfiguration())
            .ApplyConfiguration(new CourseReportEntityPartConfiguration())
            .ApplyConfiguration(new DirectionTypeReportEntityConfiguration())
            .ApplyConfiguration(new DirectionTypeReportEntityPartConfiguration())
            .ApplyConfiguration(new DirectionCodeReportEntityConfiguration())
            .ApplyConfiguration(new DirectionCodeReportPartEntityConfiguration())
            .ApplyConfiguration(new DepartmentStatisticsReportEntityConfiguration())
            .ApplyConfiguration(new DepartmentStatisticsReportPartEntityConfiguration())
            .ApplyConfiguration(new TeacherStatisticsReportPartEntityConfiguration());
}
