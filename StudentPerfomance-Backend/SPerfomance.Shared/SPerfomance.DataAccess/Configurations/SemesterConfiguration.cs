using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class SemesterConfiguration : IEntityTypeConfiguration<Semester>
{
    public void Configure(EntityTypeBuilder<Semester> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.EntityNumber).ValueGeneratedOnAdd();
        builder.ComplexProperty(
            s => s.Number,
            builder =>
            {
                builder.Property(n => n.Number).IsRequired();
            }
        );

        builder
            .HasMany(s => s.Disciplines)
            .WithOne(c => c.Semester)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => d.EntityNumber).IsUnique(true);
    }
}
