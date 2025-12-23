using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projexor.Features.Users.Models;

namespace Projexor.Data.Mapping;

public sealed class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.HasIndex(x => x.Email, "Unique_Key_Users_Email")
            .IsUnique();

        builder.Property(x => x.Phone)
            .HasColumnName("Phone")
            .HasColumnType("varchar(15)")
            .IsRequired();

        builder.HasIndex(x => x.Phone, "Unique_Key_Users_Phone")
            .IsUnique();

        builder.Property(x => x.Password)
            .HasColumnName("Password")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.Property(x => x.IsSuperAdmin)
            .HasColumnName("IsSuperAdmin")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.Active)
            .HasColumnName("Active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.HasMany(x => x.Projects)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}