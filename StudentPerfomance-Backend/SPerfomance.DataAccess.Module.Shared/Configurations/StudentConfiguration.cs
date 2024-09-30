using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder.ToTable("Students");
		builder.HasKey(s => s.Id);
		builder.Property(s => s.EntityNumber).ValueGeneratedOnAdd();
		builder.OwnsOne(s => s.Name, columnBuilder =>
		{
			columnBuilder.Property(n => n.Name).HasColumnName("Name").IsRequired();
			columnBuilder.Property(n => n.Surname).HasColumnName("Surname").IsRequired();
			columnBuilder.Property(n => n.Thirdname).HasColumnName("Thirdname").IsRequired(false);
		});
		builder.OwnsOne(s => s.Recordbook, columnBuilder =>
		{
			columnBuilder.Property(n => n.Recordbook).HasColumnName("Recordbook").IsRequired();
			columnBuilder.HasIndex(r => r.Recordbook).IsUnique(true);
		});
		builder.OwnsOne(s => s.State, columnBuilder =>
		{
			columnBuilder.Property(n => n.State).HasColumnName("State").IsRequired();
		});
		builder.HasOne(s => s.Group).WithMany(g => g.Students).IsRequired();
		builder.HasIndex(d => d.EntityNumber).IsUnique(true);
	}
}
