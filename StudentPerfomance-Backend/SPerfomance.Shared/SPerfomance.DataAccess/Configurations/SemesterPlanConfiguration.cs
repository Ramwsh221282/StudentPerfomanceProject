using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.SemesterPlans;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class SemesterPlanConfiguration : IEntityTypeConfiguration<SemesterPlan>
{
    public void Configure(EntityTypeBuilder<SemesterPlan> builder)
    {
        builder.Property(p => p.EntityNumber).ValueGeneratedOnAdd();
        builder.HasKey(p => p.Id);
        builder.OwnsOne(
            p => p.Discipline,
            onv =>
            {
                onv.Property(d => d.Name);
            }
        );

        builder.HasIndex(d => d.EntityNumber).IsUnique(true);
    }
}
