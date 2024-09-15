using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentPerfomance.DataAccess.Configurations;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(s => s.Id);
        builder.ComplexProperty(s => s.Name, columnBuilder =>
        {
            columnBuilder.Property(n => n.Name).HasColumnName("Name").IsRequired();
            columnBuilder.Property(n => n.Surname).HasColumnName("Surname").IsRequired();
            columnBuilder.Property(n => n.Thirdname).HasColumnName("Thirdname").IsRequired(false);
        });
        builder.ComplexProperty(s => s.Recordbook, columnBuilder => 
        {
            columnBuilder.Property(n => n.Recordbook).HasColumnName("Recordbook").IsRequired();            
        });
        builder.ComplexProperty(s => s.State, columnBuilder => 
        {
            columnBuilder.Property(n => n.State).HasColumnName("State").IsRequired();
        });        
        builder.HasOne(s => s.Group)
        .WithMany(g => g.Students).IsRequired();
    }
}
