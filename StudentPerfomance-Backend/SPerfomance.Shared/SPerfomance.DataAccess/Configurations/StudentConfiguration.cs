using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.EntityNumber).ValueGeneratedOnAdd();
        builder.OwnsOne(
            s => s.Name,
            columnBuilder =>
            {
                columnBuilder.Property(n => n.Name).IsRequired();
                columnBuilder.Property(n => n.Surname).IsRequired();
                columnBuilder.Property(n => n.Patronymic).IsRequired(false);
            }
        );
        builder.OwnsOne(
            s => s.Recordbook,
            columnBuilder =>
            {
                columnBuilder.Property(n => n.Recordbook).IsRequired();
                columnBuilder.HasIndex(r => r.Recordbook).IsUnique(true);
            }
        );
        builder.OwnsOne(
            s => s.State,
            columnBuilder =>
            {
                columnBuilder.Property(n => n.State).IsRequired();
            }
        );

        builder.HasIndex(d => d.EntityNumber).IsUnique(true);
    }
}
