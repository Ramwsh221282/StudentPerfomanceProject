using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class StudentAssignmentsConfiguration : IEntityTypeConfiguration<StudentAssignment>
{
    public void Configure(EntityTypeBuilder<StudentAssignment> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.EntityNumber).IsRequired();

        builder.ComplexProperty(
            a => a.Value,
            cpb =>
            {
                cpb.Property(v => v.Value).IsRequired();
            }
        );

        builder.HasOne(s => s.Student);
    }
}
