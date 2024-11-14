using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.DataAccess.Configurations;

internal sealed class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsOne(
            u => u.Email,
            cpb =>
            {
                cpb.Property(e => e.Email).IsRequired();
            }
        );

        builder.OwnsOne(
            u => u.Name,
            onb =>
            {
                onb.Property(n => n.Name).IsRequired();
                onb.Property(n => n.Surname).IsRequired();
                onb.Property(n => n.Patronymic).IsRequired(false);
            }
        );

        builder.OwnsOne(
            u => u.Role,
            onb =>
            {
                onb.Property(r => r.Role).IsRequired(true);
            }
        );

        builder.Property(u => u.AttachedRoleId).IsRequired(false);

        builder.Property(u => u.HashedPassword).IsRequired();

        builder.Property(u => u.RegisteredDate).IsRequired();
        builder.Property(u => u.LastLoginDate).IsRequired();

        builder.Property(u => u.EntityNumber).IsRequired();
        builder.HasIndex(u => u.EntityNumber).IsUnique();
    }
}
