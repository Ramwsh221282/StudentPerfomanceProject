using Microsoft.EntityFrameworkCore;
using SPerfomance.DataAccess.Configurations;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.DataAccess;

internal sealed class DatabaseContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<StudentGroup> Groups { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<TeachersDepartments> Departments { get; set; } = null!;
    public DbSet<Semester> Semesters { get; set; } = null!;
    public DbSet<SemesterPlan> SemesterPlans { get; set; } = null!;
    public DbSet<EducationDirection> EducationDirections { get; set; } = null!;
    public DbSet<EducationPlan> EducationPlans { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<AssignmentSession> Sessions { get; set; } = null!;
    public DbSet<AssignmentWeek> Weeks { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    public DbSet<StudentAssignment> StudentAssignments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentGroupConfiguration());
        modelBuilder.ApplyConfiguration(new TeachersDepartmentsConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new SemesterPlanConfiguration());
        modelBuilder.ApplyConfiguration(new SemesterConfiguration());
        modelBuilder.ApplyConfiguration(new EducationDirectionsConfiguration());
        modelBuilder.ApplyConfiguration(new EducationPlanConfiguration());
        modelBuilder.ApplyConfiguration(new UsersConfiguration());
        modelBuilder.ApplyConfiguration(new AssignmentSessionConfiguration());
        modelBuilder.ApplyConfiguration(new AssignmentWeekConfiguration());
        modelBuilder.ApplyConfiguration(new AssignmentsConfiguration());
        modelBuilder.ApplyConfiguration(new StudentAssignmentsConfiguration());
    }
}
