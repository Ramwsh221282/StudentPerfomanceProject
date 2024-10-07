using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared.Configurations;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared;

public sealed class ApplicationDb : DbContext
{
	public DbSet<Student> Students { get; set; } = null!;
	public DbSet<StudentGroup> Groups { get; set; } = null!;
	public DbSet<Teacher> Teachers { get; set; } = null!;
	public DbSet<TeachersDepartment> Departments { get; set; } = null!;
	public DbSet<Semester> Semesters { get; set; } = null!;
	public DbSet<SemesterPlan> SemesterPlans { get; set; } = null!;
	public DbSet<EducationDirection> EducationDirections { get; set; } = null!;
	public DbSet<EducationPlan> EducationPlans { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=Database.db");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new StudentConfiguration());
		modelBuilder.ApplyConfiguration(new StudentGroupConfiguration());
		modelBuilder.ApplyConfiguration(new TeachersDepartmentConfiguration());
		modelBuilder.ApplyConfiguration(new TeacherConfiguration());
		modelBuilder.ApplyConfiguration(new SemesterPlanConfiguration());
		modelBuilder.ApplyConfiguration(new SemesterConfiguration());
		modelBuilder.ApplyConfiguration(new EducationDirectionConfiguration());
		modelBuilder.ApplyConfiguration(new EducationPlanConfiguration());
	}
}
