using Microsoft.EntityFrameworkCore;
using StudentPerfomance.DataAccess.Configurations;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess;

public sealed class ApplicationDb : DbContext
{
	public DbSet<Student> Students { get; set; } = null!;
	public DbSet<StudentGroup> Groups { get; set; } = null!;
	public DbSet<Teacher> Teachers { get; set; } = null!;
	public DbSet<Discipline> Disciplines { get; set; } = null!;
	public DbSet<StudentGrade> Grades { get; set; } = null!;
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
		modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
		modelBuilder.ApplyConfiguration(new StudentGradeConfiguration());
		modelBuilder.ApplyConfiguration(new SemesterPlanConfiguration());
		modelBuilder.ApplyConfiguration(new SemesterConfiguration());
		modelBuilder.ApplyConfiguration(new EducationDirectionConfiguration());
		modelBuilder.ApplyConfiguration(new EducationPlanConfiguration());
	}
}
