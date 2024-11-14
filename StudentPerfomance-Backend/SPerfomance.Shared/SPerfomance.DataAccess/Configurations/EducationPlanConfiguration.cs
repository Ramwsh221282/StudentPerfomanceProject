using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.EducationPlans;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class EducationPlanConfiguration : IEntityTypeConfiguration<EducationPlan>
{
    public void Configure(EntityTypeBuilder<EducationPlan> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.EntityNumber).ValueGeneratedOnAdd();

        builder.ComplexProperty(
            p => p.Year,
            cpb =>
            {
                cpb.Property(y => y.Year).IsRequired();
            }
        );

        builder
            .HasMany(p => p.Groups)
            .WithOne(g => g.EducationPlan)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(p => p.Semesters)
            .WithOne(s => s.Plan)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(d => d.EntityNumber).IsUnique(true);
    }
}
