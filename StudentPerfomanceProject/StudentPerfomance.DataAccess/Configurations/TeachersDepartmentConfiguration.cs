using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.DataAccess.Configurations;

public class TeachersDepartmentConfiguration : IEntityTypeConfiguration<TeachersDepartment>
{
    public void Configure(EntityTypeBuilder<TeachersDepartment> builder)
    {        
        builder.ToTable("Departments");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired();
        builder.HasMany(d => d.Teachers).WithOne(t => t.Department);
        builder.HasIndex(d => d.Name).IsUnique();
    }
}
