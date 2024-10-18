using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.DataAccess.Module.Shared.Configurations;

internal sealed class UsersConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(u => u.Id);

		builder.ComplexProperty(u => u.Email, cpb =>
		{
			cpb.Property(e => e.Email).IsRequired();
		});

		builder.OwnsOne(u => u.Name, onb =>
		{
			onb.Property(n => n.Name).IsRequired();
			onb.Property(n => n.Surname).IsRequired();
			onb.Property(n => n.Thirdname).IsRequired(false);
		});

		builder.OwnsOne(u => u.Role, onb =>
		{
			onb.Property(r => r.Value).IsRequired(true);
		});

		builder.Property(u => u.HashedPassword).IsRequired();

		builder.Property(u => u.RegisteredDate).IsRequired();
		builder.Property(u => u.LastLoginDate).IsRequired();

		builder.Property(u => u.EntityNumber).IsRequired();
		builder.HasIndex(u => u.EntityNumber).IsUnique();
	}
}
